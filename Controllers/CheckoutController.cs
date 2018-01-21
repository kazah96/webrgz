using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rgz.Models;
using Microsoft.EntityFrameworkCore;
using rgz.Infrastructure;

namespace rgz.Controllers
{
    public class CheckoutController : Controller
    {
        Cart cart;
        IRepository repository;
        public CheckoutController(IRepository repo)
        {
            repository = repo;
        }
        public RedirectToActionResult Index()
        {
            cart = HttpContext.Session.GetJson<Cart>("Cart");
            if (cart == null)
                return RedirectToAction("Faliure");


            else return RedirectToAction("Credintals");
        }

        [HttpPost]
        public ViewResult Credintals(Client client)
        {
            cart = HttpContext.Session.GetJson<Cart>("Cart");
            repository.AddClientWithGood(client, cart);
            HttpContext.Session.SetJson("Cart", null);

            return View("Success", client);
        }

        [HttpGet]
        public ViewResult Credintals()
        {
            return View();
        }

        public ViewResult Faliure()
        {
            return View();
        }

        public ViewResult Table()
        {

            return View(repository.GetClientAndGoods());
        }

        public ViewResult Success(Client client)
        {
            return View();
        }
    }
}