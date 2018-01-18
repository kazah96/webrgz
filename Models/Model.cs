using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
namespace rgz.Models
{
    public interface IRepository
    {
        IQueryable<Good> Goods { get; }

    }

    public class DBRep : IRepository
    {
        private ShopDB repos;
        public DBRep(ShopDB context)
        {
            repos = context;
        }

        public IQueryable<Good> Goods => repos.Goods;
    }

    public class FakeRepository : IRepository
    {
        public IQueryable<Good> Goods
        {
            get
            {
                return new List<Good>{
                new Good{Name = "Tovvar1", Price=499, Description="dewded rrr"},
                new Good{Name = "Tovvar2", Price=11, Description="ebalsadas f wef"}}.AsQueryable();
            }
        }

    }

    public class ShopDB : DbContext
    {
        public DbSet<Good> Goods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder bd)
        {
            bd.UseSqlite("Data Source=Shop.db");
        }
    }

    public class Good
    {
        public int GoodId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }

    }

}