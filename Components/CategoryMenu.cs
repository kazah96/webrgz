using Microsoft.AspNetCore.Mvc;
using rgz.Models;
using System.Linq;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IRepository repository;
        public NavigationMenuViewComponent(IRepository re)
        {
            repository = re;
        }

        public IViewComponentResult Invoke()
        {
            var w = repository.Goods.GroupBy(test=>test.Adress).Select(q=>q.First().Adress);
            return View(w);
        }
    }
}