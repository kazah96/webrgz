using System.Collections.Generic;
using rgz.Models;

namespace rgz.Models.ViewModels
{
    public class ProductsListViewModel{
        public IEnumerable<Product> Products{get;set;}
        public PagingInfo PagingInfo {get;set;}
        
    }
}