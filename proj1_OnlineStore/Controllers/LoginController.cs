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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Data;
using proj1_OnlineStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace proj1_OnlineStore.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly OnlineStoreDbContext _context;
        private readonly ICustomerRepository<Customer> _customerRepository;

        public LoginController(
            OnlineStoreDbContext context,
            ILogger<LoginController> logger,
            ICustomerRepository<Customer> customerRepository)
        {
            this._logger = logger;
            this._context = context;
            this._customerRepository = customerRepository;
        }

       [Route("Login")]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View("Login", model);
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
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
                    if (customer.Login == model.UserName && customer.Password == model.Password)
                    {
                        var claims = new List<Claim> { 
                            new Claim("UserName", customer.Login),
                            new Claim(ClaimTypes.Name, Guid.NewGuid().ToString()),
                            new Claim("UserId", customer.CustomerId.ToString())
                        };
                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties();

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);
              
                        if (!string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return Redirect("/Home/Index/"); //  return to home after success
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("User", "Password does not match user name.");
                        _logger.LogInformation("Could not login, no user exists");
                        return View(model);
                    }

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

        public async Task<IActionResult> Logout()
        {
            if(HttpContext.User == null)
            {
                return RedirectToAction("Index", "Home");
            }
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public void Test()
        {
            //  return to home after success

          
        }
    }
}