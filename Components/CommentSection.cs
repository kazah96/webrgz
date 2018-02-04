using Microsoft.AspNetCore.Mvc;
using rgz.Models;
using System.Linq;

namespace rgz.Components
{
    public class CommentSectionComponent : ViewComponent
    {
        private IRepository repository;
        public CommentSectionComponent(IRepository re)
        {
            repository = re;
        }

        public IViewComponentResult Invoke(int goodId)
        {
            var w = repository.GetComments(goodId);
            return View(w);
        }     

    }
}