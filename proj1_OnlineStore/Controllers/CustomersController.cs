using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Data;
using proj1_OnlineStore.Models;

namespace proj1_OnlineStore.Controllers
{
    public class CustomersController : Controller
    {
        private ILogger<Customer> _logger;
        private ICustomerRepository<Customer> _repository;

        public CustomersController(ICustomerRepository<Customer> customerRepository, ILogger<Customer> logger)
        {
            this._logger = logger;
            this._repository = customerRepository;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetCustomers());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _repository.FindCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Details/5
        [Route("Firstname")]
        public ViewResult GetByFirst(string fname)
        {
            var customer = _repository.FindCustomerByFirstName(fname);
            return View(customer);
        }
        // GET: Customers/Details/5
        [Route("LastName")]
        public ViewResult GetByLast(string lname)
        {
            var customer = _repository.FindCustomerByLastname(lname);
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Login,Password,FirstName,LastName,PhoneNumber,DefaultLocation")] Customer customer)
        {
            
            if (ModelState.IsValid)
            {
               await _repository.Add(customer);
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
          
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _repository.FindCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Login,Password,FirstName,LastName,PhoneNumber,DefaultLocation")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateCustomer(customer);
                    _repository.Save();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException e)
                {
                    bool exists = await CustomerExists(customer.CustomerId);
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogInformation($"Could not update {customer}. Error: {e.Message}");
                        ModelState.AddModelError(string.Empty, "Unable to save changes. Try again.");
                    }
                }                
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _repository.FindCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                Customer customer = await _repository.FindCustomerById(id);
                await _repository.Delete(customer);
                _repository.Save();
            }
            catch(Exception e)
            {
                _logger.LogInformation($"Could not delete {id}. Error: {e.Message}");
                return RedirectToAction("Index");
            }
           
            return RedirectToAction("Index");
        }

        private async Task<bool> CustomerExists(int id)
        {
            return await _repository.CustomerExists(id);
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
