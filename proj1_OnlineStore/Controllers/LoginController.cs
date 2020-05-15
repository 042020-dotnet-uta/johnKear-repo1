using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//using AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Data;
using proj1_OnlineStore.Models;

namespace proj1_OnlineStore.Controllers
{
    public class LoginController : Controller
    {
        private ILogger<LoginController> _logger;
        private OnlineStoreDbContext _context;
        private ICustomerRepository<Customer> _customerRepository;

        public LoginController(
            OnlineStoreDbContext context,
            ILogger<LoginController> logger,
            ICustomerRepository<Customer> customerRepository
            )
        {
            this._logger = logger;
            this._context = context;
            this._customerRepository = customerRepository;
        }

       [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View("Login", model);
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind]Models.LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("invalid login model");
                return View(model);
            }
            else
            {
                var customer = await _customerRepository.GetCustomerByLogin(model.UserName);
                if (customer != null)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, Guid.NewGuid().ToString()) };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    Response.Redirect("/Home/Index/"); //  return to home after success
                }
                else
                {
                    ModelState.AddModelError("User", "User not found.");
                    _logger.LogInformation("Could not login, no user exists");
                    return View(model);
                }
            }
            return View();
        }

        [AllowAnonymous]
        public void Test()
        {
            //  return to home after success

          
        }
    }
}