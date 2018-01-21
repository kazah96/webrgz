using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rgz.Models;
using Microsoft.EntityFrameworkCore;
using rgz.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace rgz.Models
{

    public interface ICartService
    {
        int GetCount();
    }

    public class FakeCartService : ICartService
    {
        public int GetCount()
        {
            return 10;
        }
    }

    public class CartService : ICartService
    {  
        private static IServiceProvider provider;
        public CartService(IServiceProvider pr)
        {
            provider = pr;
        }

        public int GetCount()
        {
            ISession session = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            Cart c = session.GetJson<Cart>("Cart");
            if(c == null)
                return 0;
            return c.Lines.Count();
        }
        
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Good good, int quantity)
        {
            CartLine line = lineCollection
            .Where(p => p.Good.GoodId == good.GoodId).FirstOrDefault();
            if (line == null)
                lineCollection.Add(
                new CartLine
                {
                    Good = good,
                    Quantity = quantity
                }
            );
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Good good)
        {
            lineCollection.RemoveAll(l => l.Good.GoodId == good.GoodId);
        }
        public virtual decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e=>e.Good.Price*e.Quantity);
        }
        public virtual void Clear()
        {
            lineCollection.Clear();
        }
        public virtual IEnumerable<CartLine> Lines => lineCollection;

    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Good Good { get; set; }
        public int Quantity { get; set; }

    }
}
