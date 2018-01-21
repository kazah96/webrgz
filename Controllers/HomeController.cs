using System;
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
        public ViewResult Index(int productPage = 1)
        {

            return View(repository
            .Goods
            .OrderBy(p => p.GoodId)
            .Skip((productPage - 1) * pageSize)
            .Take(pageSize));
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
