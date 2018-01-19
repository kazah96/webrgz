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
        public RedirectToActionResult Index()
        {
            cart = HttpContext.Session.GetJson<Cart>("Cart");
            if(cart == null)
                return RedirectToAction("Faliure");
            else return RedirectToAction("Credintals");
        }

        public ViewResult Credintals()
        {
            return View();
        }

        public ViewResult Faliure()
        {
            return View();
        }

        public ViewResult Success(Client client)
        {
            return View(client);
        }
    }
}