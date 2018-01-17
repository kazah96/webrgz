using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace rgz.Models
{
    public class Product 
    {
        public int ProductID {get; set;}
        public string Name {get;set;}
        public string Description{get;set;}
        public decimal Price {get;set;}
        public string Category {get;set;}

    }

    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;

    }
    
    public class ApplicationDbContext : DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options){}

        public DbSet<Product> Products {get; set;}

    }

    public interface IProductRepository
    {
        IQueryable<Product> Products {get;}
    }

    public class FakeProductrepository : IProductRepository
    {

        public IQueryable<Product> Products =>
            new List<Product>{
                new Product { Name="Football", Price = 25},
                new Product { Name="Surf board", Price = 189},
                new Product { Name="Runnink shoes", Price = 95}

            }.AsQueryable<Product>();
        
    }

}