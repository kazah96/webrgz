using System;
using System.Collections.Generic;
using System.Linq;
using rgz.Models;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rgz.Controllers;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication.Cookies;

namespace rgz
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ShopDB>();
            services.AddDbContext<AppIdentityDbContext>();
            services.AddIdentity<AppUser,IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
    services.AddScoped<ICartService,CartService>();
            services.AddTransient<IRepository, DBRep>();
            services.AddSingleton<ISigned, IsAdm>();
            //  services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            // .AddCookie();
            services.AddMvc();
            services.AddSession();
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseSession();
                
            app.UseMvc(routes =>
            {
                routes.MapRoute(
        name: null,
        template: "{category}/Page{productPage:int}",
        defaults: new { controller = "Product", action = "List" });

                routes.MapRoute(
        name: null,
        template: "Page{productPage:int}",
        defaults: new
        {
            controller = "Home",
            action = "Index",
            productPage = 1
        }
        );
                routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new
                {
                    controller = "Home",
                    action = "List",
                    productPage = 1
                }
                );
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    productPage = 1
                });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

            });
        }
    }
}
