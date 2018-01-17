using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace rgz.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Kayak", Description = "A boat for one person", Category = "Watersports", Price = 275 },
                    new Product { Name = "Lifejacket", Description = "Protective", Category = "Watersports", Price = 248 },
                    new Product { Name = "SoccerBall", Description = "FifaApproved", Category = "Soccer", Price = 211 },
                    new Product { Name = "Corner flags", Description = "Give professional touch", Category = "Soccer", Price = 11 },
                    new Product { Name = "Stadium", Description = "Flat packed 35-000 seat stadion", Category = "Soccer", Price = 8312 },
                    new Product { Name = "Stadium", Description = "Flat packed 35-000 seat stadion", Category = "Soccer", Price = 8312 },
                    new Product {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Category = "Chess",
                        Price = 29.95m
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Category = "Chess",
                        Price = 75
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded King",
                        Category = "Chess",
                        Price = 1200
                    }
                   
                );
                context.SaveChanges();
            }
        }
    }

}