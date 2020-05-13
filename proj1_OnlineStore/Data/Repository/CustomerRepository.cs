using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class CustomerRepository : ICustomerRepository
	{

		private OnlineStoreDbContext _context;

		public CustomerRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		Customer ICustomerRepository.Add(Customer customer)
		{
			throw new NotImplementedException();
		}

		IEnumerable<Customer> ICustomerRepository.FindCustomerByFirstName(string name)
		{
			throw new NotImplementedException();
		}

		Location ICustomerRepository.GetDefaultLocation(Customer customer)
		{
			throw new NotImplementedException();
		}

		IEnumerable<Order> ICustomerRepository.GetOrderHistory(Customer customer)
		{
			throw new NotImplementedException();
		}
	}
}
