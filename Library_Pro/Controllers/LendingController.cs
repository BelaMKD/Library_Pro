using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Library_Pro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Pro.Controllers
{
    public class LendingController : Controller
    {
        private readonly ILendingData lendingData;
        private readonly IClientData clientData;
        private readonly IBookCopiesData bookCopiesData;
        private readonly IBookData bookData;
        private readonly ILibraryData libraryData;

        public LendingController(ILendingData lendingData, IClientData clientData, IBookCopiesData bookCopiesData, IBookData bookData, ILibraryData libraryData)
        {
            this.lendingData = lendingData;
            this.clientData = clientData;
            this.bookCopiesData = bookCopiesData;
            this.bookData = bookData;
            this.libraryData = libraryData;
        }
        public IActionResult Returned(int libraryId)
        {
            var model = new ReturnedViewModel()
            {
                Lendings = lendingData.GetLendingsReturned(libraryId),
                LibraryId = libraryId
            };
            return View(model);
        }
        public IActionResult NotReturned(int libraryId)
        {
            var model = new NotReturnedViewModel()
            {
                Lendings = lendingData.GetLendingsNotReturned(libraryId),
                LibraryId = libraryId
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int bookCopiesId)
        {
            var bookCopies = bookCopiesData.GetBookCopiesById(bookCopiesId);
            var model = new LendingCreateEditViewModel();
            model.BookCopiesId = bookCopiesId;
            model.Lending = new Lending()
            {
                BookId = bookCopies.BookId,
                Book = bookCopies.Book
            };
            model.LibraryId = bookCopies.LibraryId;
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
                var bookCopies = bookCopiesData.GetBookCopiesById(model.BookCopiesId);
                if (model.Lending.DatumVratena==null)
                {
                    bookCopies.NumberOfCopies -= 1;
                }
                lendingData.Commit();
                TempData["Message"] = "The object is created";
                return RedirectToAction("Detail", "Library", new { libraryId = model.LibraryId });
            }
            model.Clients = clientData.GetClients().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int lendingId, int libraryId, int bookCopiesId)
        {
            var model = new LendingCreateEditViewModel()
            {
                Lending = lendingData.GetLendingById(lendingId),
                LibraryId = libraryId,
                BookCopiesId = bookCopiesId,
                Clients = clientData.GetClients().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };
            if (model.Lending == null)
            {
                return RedirectToAction("NotReturned", "Lending", new { libraryId = model.LibraryId });
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(LendingCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tempLending = lendingData.GetLendingById(model.Lending.Id);
                tempLending.Book = bookData.GetBookByid(model.Lending.BookId);
                tempLending.Client = clientData.GetClientById(model.Lending.ClientId);
                tempLending.DatumZajmuvanje = model.Lending.DatumZajmuvanje;
                tempLending.DatumVratena = model.Lending.DatumVratena;
                var bookCopies = bookCopiesData.GetBookCopiesById(model.BookCopiesId);
                if (model.Lending.DatumVratena!=null)
                {
                    bookCopies.NumberOfCopies += 1;
                }
                lendingData.UpdateLending(tempLending);
                lendingData.Commit();
                TempData["Message"] = "The object is updated";
                return RedirectToAction("NotReturned", "Lending", new { libraryId = model.LibraryId });
            } 
            return View(model);
        }
    }
        
}