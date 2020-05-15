using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class CustomerRepository : ICustomerRepository<Customer>, IDisposable
	{

		private OnlineStoreDbContext _context;

		public CustomerRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		public async Task<Customer> Add(Customer customer)
		{
			var entity = _context.Add(customer).Entity;
			await _context.SaveChangesAsync();
			return entity;
		}

		async Task<IEnumerable<Customer>> ICustomerRepository<Customer>.FindCustomerByFirstName(string fname)
		{
			return await _context.Customers.Where(c => c.FirstName.ToUpper() == fname.ToUpper()).ToListAsync();
		}

		public async Task<IEnumerable<Customer>> FindCustomerByLastname(string lname)
		{
			return await _context.Customers.Where(c => c.LastName.ToUpper() == lname.ToUpper()).ToListAsync();
		}

		public async Task<IEnumerable<Customer>> FindCustomerByFullname(string name)
		{
			var _name = name.Split(' ');
			var customers = await _context.Customers.Where(c => c.FirstName == _name[0] && c.LastName == _name[1]).ToListAsync();
			return customers;
		}

		public async Task<Location> GetDefaultLocation(Customer customer)
		{
			return await _context.Locations.Where(l => l.LocationId == customer.DefaultLocation).FirstOrDefaultAsync();
		}

		async Task ICustomerRepository<Customer>.SetDefaultLocation(Customer customer, Location location)
		{
			var _customer = await _context.Customers.SingleAsync(c => c.CustomerId == customer.CustomerId);
			_customer.DefaultLocation = location.LocationId;
			_context.Update(_customer);
			await _context.SaveChangesAsync();
		}

		async Task ICustomerRepository<Customer>.Delete(Customer customer)
		{
			var ent = _context.Remove(customer).Entity;
			await _context.SaveChangesAsync();
		}

		async Task<Customer> ICustomerRepository<Customer>.UpdateCustomer(Customer customer)
		{
			var _customer = await _context.Customers.SingleAsync(c => c.CustomerId == customer.CustomerId);
			if (_customer != null)
			{
				_customer.FirstName = customer.FirstName;
				_customer.LastName = customer.LastName;
				_customer.PhoneNumber = customer.PhoneNumber;
				_customer.DefaultLocation = customer.DefaultLocation;
				_context.Update(_customer);
				await _context.SaveChangesAsync();
			}
			return _customer;
		}

		void ICustomerRepository<Customer>.Save()
		{
			_context.SaveChangesAsync();
		}


		public async Task<Customer> FindCustomerById(int? id)
		{
			return await _context.Customers.Where(c => c.CustomerId == id).FirstOrDefaultAsync();
		}

		public async Task<bool> CustomerExists(int id)
		{
			bool exists = false;
			var customer = await _context.Customers.Where(c => c.CustomerId == id).FirstOrDefaultAsync();
			if (!(customer == null))
			{
				exists = true;
			}
			return exists;
		}
		
		public async Task<IEnumerable<Customer>> GetCustomers()
		{
			return await _context.Customers.ToListAsync();
		}

		public async Task<Customer> GetCustomerByLogin(string login)
		{
			return await _context.Customers.Where(c => c.Login == login).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Order>> GetOrderHistory(Customer customer)
		{
			return await _context.Orders.Where(o => o.CustomerId == customer.CustomerId).ToListAsync();
		}

		public async Task<bool> LoginExists(string login)
		{
			bool exists = false;
			var _login = await _context.Customers.Where(c => c.Login == login).FirstOrDefaultAsync();
			if (!(_login == null))
			{
				exists = true;
			}
			return exists;
		}
		
		/// <summary>
		/// implements the IDisposable
		/// </summary>
		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if(disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}		
	}
}
