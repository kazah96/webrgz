using Microsoft.AspNetCore.Mvc;
using rgz.Models;
using rgz.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace rgz.Controllers
{
    public class ProductController : Controller{

        public int PageSize=5;
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public ViewResult Index(int productPage = 1)
        {
            
            return View(new ProductsListViewModel{ Products = repository.Products.OrderBy(p=>p.ProductID).Skip((productPage-1)*PageSize).Take(PageSize),
             PagingInfo = new PagingInfo{CurrentPage=productPage,ItemsPerPage=PageSize, TotalItems=repository.Products.Count()}});
        }
    }
}