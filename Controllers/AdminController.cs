using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rgz.Models;
using Microsoft.EntityFrameworkCore;
using rgz.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace rgz.Models
{


    [Authorize]
    public class AdminController : Controller
    {
        private IRepository repository;
        public AdminController(IRepository rep)
        {
            repository = rep;

        }
        public IActionResult Index()
        {
            return RedirectToAction("Orders");
        }
        public ViewResult Orders()
        {
            return View(repository.GetClientAndGoods());
        }
        public ViewResult Goods()
        {
            return View(repository);
        }


        [HttpGet]
        public ViewResult AddGood()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGood(AddGoodModel mdl)
        {
            if (ModelState.IsValid)
            {
                repository.AddGood(
                    new Good
                    {
                        Description = mdl.Description,
                        Name = mdl.Name,
                        Price = mdl.Price

                    }
                );
                return Redirect("/Admin/Goods");
            }
            return View();
        }


        public ViewResult Edit(int goodId)
        {
            AddGoodModel mdl = new AddGoodModel();
            var good = repository.Goods.FirstOrDefault(w => w.GoodId == goodId);
            mdl.Description = good.Description;
            mdl.Name = good.Name;
            mdl.Price = good.Price;
            mdl.id = good.GoodId;

            return View(mdl);
        }

        public IActionResult EditGood(AddGoodModel addGood)
        {

            if (ModelState.IsValid)
            {
                var r = repository.Goods.FirstOrDefault(w => w.GoodId == addGood.id);
                r.Description = addGood.Description;
                r.Name = addGood.Name;
                r.Price = addGood.Price;
                repository.SaveChanges();
                return Redirect("/Admin/Goods");
            }
            return View("Checkout/Failure");
        }
        public IActionResult Delete(int goodId)
        {
            repository.DeleteGood(goodId);

            return RedirectToAction("Goods");
        }
    }
}