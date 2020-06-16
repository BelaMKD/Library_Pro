using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Library_Pro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Pro.Controllers
{
    public class LendingController : Controller
    {
        private readonly ILendingData lendingData;
        private readonly IClientData clientData;
        private readonly IBookCopiesData bookCopiesData;
        private readonly IBookData bookData;

        public LendingController(ILendingData lendingData, IClientData clientData, IBookCopiesData bookCopiesData, IBookData bookData)
        {
            this.lendingData = lendingData;
            this.clientData = clientData;
            this.bookCopiesData = bookCopiesData;
            this.bookData = bookData;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(int bookCopiesId)
        {
            var bookCopies = bookCopiesData.GetBookCopiesById(bookCopiesId);
            var model = new LendingCreateEditViewModel();
            model.bookCopiesId = bookCopiesId;
            model.Lending = new Lending()
            {
                BookId = bookCopies.BookId,
                Book = bookCopies.Book
            };
            model.libraryId = bookCopies.LibraryId;
            model.Clients = clientData.GetClients().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(LendingCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Lending.Client = clientData.GetClientById(model.Lending.ClientId);
                model.Lending.Book = bookData.GetBookByid(model.Lending.BookId);
                lendingData.CreateLending(model.Lending);
                var bookCopies = bookCopiesData.GetBookCopiesById(model.bookCopiesId);
                bookCopies.NumberOfCopies -= 1;
                lendingData.Commit();
                TempData["Message"] = "The object is created";
                return RedirectToAction("Detail", "Library", new { libraryId = model.libraryId });
            }
            model.Clients = clientData.GetClients().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }
    }
        
}