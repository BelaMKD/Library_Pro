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
    public class BookController : Controller
    {
        private readonly IBookData bookData;
        private readonly IPublisherData publisherData;

        public BookController(IBookData bookData, IPublisherData publisherData)
        {
            this.bookData = bookData;
            this.publisherData = publisherData;
        }
        public IActionResult Index()
        {
            var model = new BookIndexViewModel() { Books = bookData.GetBooks() };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new BookCreateEditViewModel()
            {
                Book = new Book(),
                AddPublisher = publisherData.GetPublisers().Select(x => new SelectListItem
                {
                    Selected = false,
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(BookCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var publisher in model.AddPublisher)
                {
                    if (publisher.Selected)
                    {
                        model.Book.Publisers.Add(publisherData.GetPubliserById(Convert.ToInt32(publisher.Value)));
                    }
                }
                bookData.CreateBook(model.Book);
                bookData.Commit();
                TempData["Message"] = "The object is edited";
                return RedirectToAction("Index");
            }
            model.AddPublisher = publisherData.GetPublisers().Select(x => new SelectListItem
            {
                Selected = false,
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int bookId)
        {
            var model = new BookCreateEditViewModel();
            model.Book = bookData.GetBookByid(bookId);
            List<SelectListItem> checkedPublishers = new List<SelectListItem>();
            foreach (var publisher in publisherData.GetPublisers())
            {
                checkedPublishers.Add(new SelectListItem
                {
                    Value = publisher.Id.ToString(),
                    Selected = false,
                    Text = publisher.Name
                });
            }
            foreach (var item in checkedPublishers)
            {
                foreach (var publisher in model.Book.Publisers)
                {
                    if (Convert.ToInt32(item.Value) == publisher.Id)
                    {
                        item.Selected = true;
                    }
                }
            }
            model.AddPublisher = checkedPublishers;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(BookCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tempBook = bookData.GetBookByid(model.Book.Id);
                tempBook.Title = model.Book.Title;
                tempBook.NumberOfPages = model.Book.NumberOfPages;
                tempBook.YearOfIssue = model.Book.YearOfIssue;
                if (tempBook.Publisers.Any())
                {
                    tempBook.Publisers.Clear();

                }
                foreach (var publisher in model.AddPublisher)
                {
                    if (publisher.Selected)
                    {
                        tempBook.Publisers.Add(publisherData.GetPubliserById(Convert.ToInt32(publisher.Value)));
                    }
                }
                bookData.UpdateBook(tempBook);
                bookData.Commit();
                TempData["Message"] = "The object is edited";
                return RedirectToAction("Index");
            }
            List<SelectListItem> checkedPublishers = new List<SelectListItem>();
            foreach (var publisher in publisherData.GetPublisers())
            {
                checkedPublishers.Add(new SelectListItem
                {
                    Value = publisher.Id.ToString(),
                    Selected = false,
                    Text = publisher.Name
                });
            }
            foreach (var item in checkedPublishers)
            {
                foreach (var publisher in model.Book.Publisers)
                {
                    if (Convert.ToInt32(item.Value) == publisher.Id)
                    {
                        item.Selected = true;
                    }
                }
            }
            model.AddPublisher = checkedPublishers;
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int bookId)
        {
            var book = bookData.GetBookByid(bookId);
            if (book!=null)
            {
                return View(book);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Book book)
        {
            var tempBook = bookData.DeleteBook(book.Id);
            if (tempBook==null)
            {
                return RedirectToAction("Index");
            }
            bookData.Commit();
            TempData["Message"] = "The object is deleted";
            return RedirectToAction("Index");
        }
    }
}