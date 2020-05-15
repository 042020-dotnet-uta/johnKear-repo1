using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using proj1_OnlineStore.Areas.Identity.Data;
using proj1_OnlineStore.Data;

[assembly: HostingStartup(typeof(proj1_OnlineStore.Areas.Identity.IdentityHostingStartup))]
namespace proj1_OnlineStore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<proj1_OnlineStoreContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("proj1_OnlineStoreContextConnection")));

                services.AddDefaultIdentity<proj1_OnlineStoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<proj1_OnlineStoreContext>();
            });
        }
    }
}