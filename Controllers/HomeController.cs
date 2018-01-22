﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rgz.Models;
using Microsoft.EntityFrameworkCore;
using rgz.Infrastructure;

using Microsoft.AspNetCore.Authorization;

namespace rgz.Controllers
{
    
    public class HomeController : Controller
    {
        private IRepository repository;
        public int pageSize = 4;

      ///  [Authorize]

        [HttpGet]
        public ViewResult Index(string street)
        {
            List<Good> w;
            if(street != null)
            {
                w = repository.Goods.Where(y=>y.Adress == street).ToList();
            }
            else
                w = repository.Goods.ToList();

            return View(w);
        }
        public HomeController(IRepository repo)
        {
            repository = repo;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult ViewGood(int id)
        {

           return View(repository.Goods.FirstOrDefault(e=>e.GoodId==id));
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
