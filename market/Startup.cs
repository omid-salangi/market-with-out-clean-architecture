using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using market.Data;
using market.Data.Repositories;
using market.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace market
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
            services.AddControllersWithViews();
            services.AddRazorPages();

            #region Db Context

            services.AddDbContext<MarketContext>(options => // add a data base
            options.UseSqlServer("Data Source=.;Initial Catalog=Market_DB;Integrated security=true")
            );

            #endregion

            #region IOC

            services.AddScoped<IGroupRepositories, GroupRepositories>();// for repository any one wants IGroupRepository you give it on constructor
                                                                        //services.AddTransient<IGroupRepositories, GroupRepositories>(); //for each request will make
                                                                        // services.AddSingleton<IGroupRepositories, GroupRepositories>(); // it will make one for entire of project
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region Authentication

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) //for cookie using
                .AddCookie(option =>
                {
                    option.LoginPath = "/Accounts/login";
                    option.LogoutPath = "/Accounts/LogOut";
                    option.ExpireTimeSpan = TimeSpan.FromDays(10);
                });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();
            app.UseAuthentication(); // identity

            app.UseAuthorization(); // access level
            app.Use(async (context, next) => // for access to admin
                {
                    if (context.Request.Path.StartsWithSegments("/Admin"))
                    {
                        if (!context.User.Identity.IsAuthenticated)
                        {
                            context.Response.Redirect("/Accounts/Login");
                        }
                        else if (!bool.Parse(context.User.FindFirstValue("IsAdmin")))
                        {
                            context.Response.Redirect("/Accounts/permit");
                        }


                    }

                    await next.Invoke();
                }
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
