using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Library_Pro.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientData clientData;

        public ClientController(IClientData clientData)
        {
            this.clientData = clientData;
        }
        public IActionResult Index()
        {
            var clients = clientData.GetClients();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var client = new Client();
            return View(client);
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                clientData.CreateClient(client);
                clientData.Commit();
                TempData["Message"] = "The object is created";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int clientId)
        {
            var client = clientData.GetClientById(clientId);
            if (client == null)
            {
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                clientData.UpdateClient(client);
                clientData.Commit();
                TempData["Message"] = "The object is edited";
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpGet]
        public IActionResult Delete(int clientId)
        {
            var client = clientData.GetClientById(clientId);
            if (client == null)
            {
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpPost]
        public IActionResult Delete(Client client)
        {
            var tempClient = clientData.DeleteClient(client.Id);
            if (tempClient == null)
            {
                return RedirectToAction("Index");
            }
            clientData.Commit();
            TempData["Message"] = "The object is deleted";
            return RedirectToAction("Index");
        }
    }
}