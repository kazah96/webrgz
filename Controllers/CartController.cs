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
    public class CartController : Controller
    {
        private IRepository repository;
        public CartController(IRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(GetCart());
        }
        
        public RedirectToActionResult AddToCart(int goodId, string returnUrl)
        {
            Good good = repository.Goods.FirstOrDefault(w => w.GoodId == goodId);
            if (good != null)
            {
                Cart cart = GetCart();
                cart.AddItem(good, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index","Home", new { returnUrl });
        }
        public RedirectToActionResult RemoveAll()
        {
            HttpContext.Session.SetJson("Cart", null);
            
            return RedirectToAction("Index","Cart");

        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}