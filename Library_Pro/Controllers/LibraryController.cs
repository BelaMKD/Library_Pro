using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Library_Pro.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Pro.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryData libraryData;

        public LibraryController(ILibraryData libraryData)
        {
            this.libraryData = libraryData;
        }

        public IActionResult Index()
        {
            var model = new LibraryIndexViewModel();
            model.Libraries = libraryData.GetLibraries();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var library = new Library();
            return View(library);
        }

        [HttpPost]
        public IActionResult Create(Library library)
        {
            if (ModelState.IsValid)
            {
                libraryData.CreateLibrary(library);
                libraryData.Commit();
                TempData["Message"] = "The object is created";
                return RedirectToAction("Index");
            }
            return View(library);
        }

        [HttpGet]
        public IActionResult Edit(int libraryId)
        {
            var library = libraryData.GetLibraryById(libraryId);
            if (library == null)
            {
                return RedirectToAction("Index");
            }
            return View(library);
        }

        [HttpPost]
        public IActionResult Edit(Library library)
        {
            if (ModelState.IsValid)
            {
                libraryData.UpdateLibrary(library);
                libraryData.Commit();
                TempData["Message"] = "The object is edited";
                return RedirectToAction("Index");
            }
            return View(library);
        }

        [HttpGet]
        public IActionResult Delete(int libraryId)
        {
            var library = libraryData.GetLibraryById(libraryId);
            if (library == null)
            {
                return RedirectToAction("Index");
            }
            return View(library);
        }

        [HttpPost]
        public IActionResult Delete(Library library)
        {
            var tempLibrary = libraryData.DeleteLibrary(library.Id);
            if (tempLibrary == null)
            {
                return RedirectToAction("Index");
            }
            libraryData.Commit();
            TempData["Message"] = "The object is deleted";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Detail(int libraryId)
        {
            var library = libraryData.GetLibraryById(libraryId);
            if (library == null)
            {
                return RedirectToAction("Index");
            }
            return View(library);
        }
    }
}