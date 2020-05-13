using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Data;
using proj1_OnlineStore.Data.Repository;

namespace proj1_OnlineStore.Controllers
{
    public class LoginController : Controller
    {

        private OnlineStoreDbContext _context;
        private ILogger<LoginController> _logger;
        private ICustomerRepository _customerRepository;

        public LoginController (
            OnlineStoreDbContext context,
            ILogger<LoginController> logger,
            ICustomerRepository customerRepository
            )
        {
            this._context = context;
            this._logger = logger;
            this._customerRepository = customerRepository;
        }

        [Route("Login")]
        public async Task<IActionResult> Index()
        {
            var model = new Models.LoginViewModel();
            return View("Index", model);
        }

        [Route("Login/TryLogin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TryLogin([Bind]Models.LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this._logger.LogCritical("invalid login model");
            }
            else
            {
                this._logger.LogInformation("valid login model");
            }
            this._logger.LogTrace($"model username={model.UserName}");
            return View("Index", model);
        }
        

    }
}