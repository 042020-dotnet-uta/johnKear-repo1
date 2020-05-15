using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using proj1_OnlineStore.Data;
using proj1_OnlineStore.Data.Repository;
using proj1_OnlineStore.Models;

namespace proj1_OnlineStore
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
			//services.AddRazorPages().AddRazorRuntimeCompilation();
			services.AddRazorPages();

			services.AddDbContext<OnlineStoreDbContext>(options => options
			.UseSqlServer(Configuration.GetConnectionString("OnlineStoreDbContext")));
			
			//  Add Logging
			services.AddLogging(logger =>
			{
				Host.CreateDefaultBuilder()
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
				});
			});

			//  Configure cookie authentication policy
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/Login/";
					options.AccessDeniedPath = "/Home/Unathorized/";
					options.LogoutPath = "/Home/Logout/";
					options.Cookie.HttpOnly = true;
					options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				});

			services.AddMvc(options =>
			{
				options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
			});

			//  Add Repositories
			services.AddScoped<ICustomerRepository<Customer>, CustomerRepository>();
			services.AddScoped<ILocationRepository<Location>, LocationRepository>();
			services.AddScoped<IOrderRepository<Order>, OrderRepository>();
			services.AddScoped<IProductRepository<Product>, ProductRepository>();
			/*services.AddControllers(config =>
			{
				//  using Microsoft.AspNetCore.Mvc.Authorization;
				//  using Microsoft.AspNetCore.Authorization;
				var policy = new AuthorizationPolicyBuilder()
											.RequireAuthenticatedUser()
											.Build();

				config.Filters.Add(new AuthorizationFilter(policy));
			});*/
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
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
