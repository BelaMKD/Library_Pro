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
    public class BookCopiesController : Controller
    {
        private readonly IBookCopiesData bookCopiesData;
        private readonly ILibraryData libraryData;
        private readonly IBookData bookData;

        public BookCopiesController(IBookCopiesData bookCopiesData, ILibraryData libraryData, IBookData bookData)
        {
            this.bookCopiesData = bookCopiesData;
            this.libraryData = libraryData;
            this.bookData = bookData;
        }

        [HttpGet]
        public IActionResult Create(int libraryId)
        {
            var model = new BookCopiesCreateEditViewModel();
            model.BookCopies = new BookCopies()
            {
                LibraryId = libraryId,
                Library = libraryData.GetLibraryById(libraryId)
            };

            if (model.BookCopies.Library.BookCopies.Any())
            {
                var booksInLibrary = new List<Book>();
                foreach (var book in model.BookCopies.Library.BookCopies)
                {
                    booksInLibrary.Add(book.Book);
                }

                foreach (var book in bookData.GetBooks())
                {
                    if (!booksInLibrary.Contains(book))
                    {
                        model.Books.Add(new SelectListItem
                        {
                            Value = book.Id.ToString(),
                            Text = book.Title
                        });
                    }
                }
            }
            else
            {
                model.Books = bookData.GetBooks().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Title
                }).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BookCopiesCreateEditViewModel model)
        {
            model.BookCopies.Library = libraryData.GetLibraryById(model.BookCopies.LibraryId);
            if (ModelState.IsValid)
            {
                model.BookCopies.Book = bookData.GetBookByid(model.BookCopies.BookId);
                bookCopiesData.CreateBookCopies(model.BookCopies);
                bookCopiesData.Commit();
                TempData["Message"] = "The Object is created";
                return RedirectToAction("Detail", "Library", new { libraryId = model.BookCopies.LibraryId });
            }
            if (model.BookCopies.Library.BookCopies.Any())
            {
                var booksInLibrary = new List<Book>();
                foreach (var book in model.BookCopies.Library.BookCopies)
                {
                    booksInLibrary.Add(book.Book);
                }

                foreach (var book in bookData.GetBooks())
                {
                    if (!booksInLibrary.Contains(book))
                    {
                        model.Books.Add(new SelectListItem
                        {
                            Value = book.Id.ToString(),
                            Text = book.Title
                        });
                    }
                }
            }
            else
            {
                model.Books = bookData.GetBooks().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Title
                }).ToList();
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int bookCopiesId)
        {
            var model = new BookCopiesCreateEditViewModel()
            {
                BookCopies = bookCopiesData.GetBookCopiesById(bookCopiesId)
            };
            if (model.BookCopies == null)
            {
                return RedirectToAction("Detail", "Library", new { libraryId = model.BookCopies.LibraryId });
            }
            model.BookCopies.Library = libraryData.GetLibraryById(model.BookCopies.LibraryId);
            var booksInLibrary = new List<Book>();
            foreach (var book in model.BookCopies.Library.BookCopies.Where(x => x.BookId != model.BookCopies.BookId))
            {
                booksInLibrary.Add(book.Book);
            }
            foreach (var book in bookData.GetBooks())
            {
                if (!booksInLibrary.Contains(book))
                {
                    model.Books.Add(new SelectListItem
                    {
                        Value = book.Id.ToString(),
                        Text = book.Title
                    });
                }
            }
            model.Selected = new List<int>(model.Books.Count);
            foreach (var book in model.Books)
            {
                model.Selected.Add(Convert.ToInt32(book.Value));
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BookCopiesCreateEditViewModel model)
        {
            model.BookCopies.Library = libraryData.GetLibraryById(model.BookCopies.LibraryId);
            if (ModelState.IsValid)
            {
                var tempBookCopies = bookCopiesData.GetBookCopiesById(model.BookCopies.Id);
                tempBookCopies.BookId = model.BookCopies.BookId;
                tempBookCopies.Book = bookData.GetBookByid(model.BookCopies.BookId);
                tempBookCopies.NumberOfCopies = model.BookCopies.NumberOfCopies;
                bookCopiesData.UpdateBookCopies(tempBookCopies);
                bookCopiesData.Commit();
                TempData["Message"] = "The object is edited";
                return RedirectToAction("Detail", "Library", new { libraryId = model.BookCopies.LibraryId });
            }
            var booksInLibrary = new List<Book>();
            foreach (var bookId in model.Selected)
            {
                booksInLibrary.Add(bookData.GetBookByid(bookId));
            }
            foreach (var book in booksInLibrary)
            {
                model.Books.Add(new SelectListItem
                {
                    Value = book.Id.ToString(),
                    Text = book.Title
                });
            } 
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int bookCopiesId)
        {
            var bookCopies = bookCopiesData.GetBookCopiesById(bookCopiesId);
            if (bookCopies == null)
            {
                return RedirectToAction("Detail", "Library", new { libraryId = bookCopies.LibraryId });
            }
            return View(bookCopies);
        }

        [HttpPost]
        public IActionResult Delete(BookCopies bookCopies)
        {
            var tempBookCopies = bookCopiesData.DeleteBookCopies(bookCopies.Id);
            if (tempBookCopies == null)
            {
                return RedirectToAction("Detail", "Library", new { libraryId = bookCopies.LibraryId });
            }
            bookCopiesData.Commit();
            TempData["Message"] = "The object is deleted";
            return RedirectToAction("Detail", "Library", new { libraryId = bookCopies.LibraryId });
        }
    }
}