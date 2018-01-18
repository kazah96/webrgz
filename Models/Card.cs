using System.Collections.Generic;
using System.Linq;
using rgz.Models;

namespace rgz.Models
{
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
