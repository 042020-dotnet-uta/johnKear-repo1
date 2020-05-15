using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data
{
	/// <summary>
	/// Not implemented
	/// </summary>
	interface IRepository
	{
	}

	public interface ICustomerRepository<T> : IDisposable
	{
		Task<IEnumerable<Customer>>FindCustomerByFirstName(string fname);
		Task<IEnumerable<Customer>> FindCustomerByLastname(string lname);
		Task<IEnumerable<Customer>> FindCustomerByFullname(string name);
		Task<IEnumerable<Order>> GetOrderHistory(Customer customer);
		Task<Customer> FindCustomerById(int? id);
		Task<Location> GetDefaultLocation(Customer customer);
		Task SetDefaultLocation(Customer customer, Location location);
		Task<Customer> Add(Customer customer);
		Task Delete(Customer customer);
		Task<bool> LoginExists(string login);
		Task<bool> CustomerExists(int id);
		Task<Customer> GetCustomerByLogin(string login);
		Task<IEnumerable<Customer>> GetCustomers();
		Task<Customer> UpdateCustomer(Customer customer);
		void Save();
	}

	public interface ILocationRepository<T>
	{
		Task<T> GetLocations();
		Task<T> GetInventory(Location location);
	}
	
	public interface IOrderRepository<T>
	{
		Task<IEnumerable<Order>> GetOrderHistory(Customer customer);
		Task<IEnumerable<OrderLineItem>> GetLineItems(Order order);
		Task<T> SetLineItemQuantity(OrderLineItem lineItem, int newQuantity);
		Task<T> RemoveLineItem(OrderLineItem lineItem);
	}

	public interface IProductRepository<T>
	{
		Task<T> GetProductById(int id);
		Task<IEnumerable<Product>> GetProductsByLocation(Location location);
	}

}
