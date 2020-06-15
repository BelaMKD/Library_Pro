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
    public class PublisherController : Controller
    {
        private readonly IPublisherData publisherData;

        public PublisherController(IPublisherData publisherData)
        {
            this.publisherData = publisherData;
        }
        public IActionResult Index()
        {
            var model = new PublishersIndexViewModel();
            model.Publishers = publisherData.GetPublisers();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var publisher = new Publisher();
            return View(publisher);
        }
        [HttpPost]
        public IActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                publisherData.CreatePublisher(publisher);
                publisherData.Commit();
                TempData["Message"] = "The object is created";
                return RedirectToAction("Index");
            }
            return View(publisher);
        }
        [HttpGet]
        public IActionResult Edit(int publisherId)
        {
            var publisher = publisherData.GetPubliserById(publisherId);
            if (publisher == null)
            {
                return RedirectToAction("Index");
            }
            return View(publisher);
        }
        [HttpPost]
        public IActionResult Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                publisherData.UpdatePublisher(publisher);
                publisherData.Commit();
                TempData["Message"] = "The object is edited";
                return RedirectToAction("Index");
            }
            return View(publisher);
        }
        [HttpGet]
        public IActionResult Delete(int publisherId)
        {
            var publisher = publisherData.GetPubliserById(publisherId);
            if (publisher == null)
            {
                return RedirectToAction("Index");
            }
            return View(publisher);
        }
        public IActionResult Delete(Publisher publisher)
        {
            var tempPublisher = publisherData.DeletePublisher(publisher.Id);
            if (tempPublisher == null)
            {
                RedirectToAction("Index");
            }
            publisherData.Commit();
            TempData["Message"] = "The object is deleted";
            return RedirectToAction("Index");
        }
    }
}