using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
namespace rgz.Models
{
    public interface IRepository
    {
        IQueryable<Good> Goods { get; }
        IQueryable<Client> Clients { get; }

        void AddClientWithGood(Client client, Cart cart);
        List<ClientAndGoods> GetClientAndGoods();

        void AddGood(Good good);

        void SaveChanges();
        void DeleteGood(int id);

    }

    public class DBRep : IRepository
    {
        private ShopDB repos;
        public DBRep(ShopDB context)
        {
            repos = context;
            repos.Goods.Load();
        }
        public IQueryable<Good> Goods => repos.Goods;

        public void AddGood(Good good)
        {
            repos.Add(good);
            repos.SaveChanges();
        }
        public void SaveChanges()
        {
            repos.SaveChanges();
        }
        public void DeleteGood(int id)
        {
            repos.Remove(repos.Goods.FirstOrDefault(w=>w.GoodId==id));
            SaveChanges();
        }
        public IQueryable<Client> Clients
        {
            get
            {
                repos.Clients.Include(w => w.ClientGood);
                Goods.Load();
                return repos.Clients;
            }
        }
        public void AddClientWithGood(Client client, Cart cart)
        {
            
            foreach (var t in cart.Lines)
            {
                
                repos.Add(new ClientGood { Client = client, Good = t.Good, Quantity = t.Quantity});
            }
            repos.SaveChanges();
        }
        public List<ClientAndGoods> GetClientAndGoods()
        {
            var r = new List<ClientAndGoods>();

            var cc = repos.Clients.Include(f => f.ClientGood);
            repos.Goods.Load();

            foreach (var t in cc)
            {

                System.Console.WriteLine(t.Name);
                var hh = new ClientAndGoods{Client=t};
                hh.Goods = new List<Good>();

                foreach (var i in t.ClientGood)
                {
                    hh.Goods.Add(i.Good);
                    System.Console.WriteLine(i.Good.Name);
                    
                }
                r.Add(hh);
            }

            return r;

        }

    }

    public class Good
    {
        public Good()
        {
            ClientGood = new List<ClientGood>();
        }
        public int GoodId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }  
        public string ImgPath { get; set; }
        public string StreetNumber{get;set;}
        public ICollection<ClientGood> ClientGood { get; set; }

    }
    public class ClientGood
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int GoodId { get; set; }
        public Good Good { get; set; }
        public int Quantity{get;set;}
      

    }   

    public class Client
    {
        public Client()
        {
            ClientGood = new List<ClientGood>();
        }
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Веведите адрес")]
        public string Adress { get; set; }
        public virtual ICollection<ClientGood> ClientGood { get; set; }

    }

    public class ClientAndGoods
    {
        public Client Client;
        public List<Good> Goods;    
    }

    public class UserDB : DbContext
    {
        public DbSet<User> Users {get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder opdb)
        {
            opdb.UseSqlite("Data Source = Users.db");
        }
        
    }

    public class User
    {
        public int UserId{get;set;}
        public string Login{get;set;}
        public string Password{get;set;}
        public int AccessLevel{get;set;}
        
    }

    public class ShopDB : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Good> Goods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder opdb)
        {
            opdb.UseSqlite("Data Source = Shop.db");
        }
        protected override void OnModelCreating(ModelBuilder md)
        {
            md.Entity<ClientGood>().HasKey(w => new { w.ClientId, w.GoodId });
            md.Entity<ClientGood>().HasOne(w => w.Client).WithMany(w => w.ClientGood).HasForeignKey(q => q.ClientId);
            md.Entity<ClientGood>().HasOne(w => w.Good).WithMany(w => w.ClientGood).HasForeignKey(q => q.GoodId);
        }
    }

    public class Login
    {
        public string User{get;set;}
        public string Password{get;set;}
    }
}