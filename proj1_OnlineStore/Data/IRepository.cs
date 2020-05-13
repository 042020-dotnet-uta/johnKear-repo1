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

	public interface ICustomerRepository
	{
		Location GetDefaultLocation(Customer customer);
		Customer Add(Customer customer);
		IEnumerable<Order> GetOrderHistory(Customer customer);
		IEnumerable<Customer> FindCustomerByFirstName(string name);
	}

	public interface ILocationRepository
	{
		IEnumerable<Location> GetLocations();
		IEnumerable<Product> GetInventory(Location location);
	}
	
	public interface IOrderRepository
	{
		IEnumerable<Order> GetOrderHistory(Customer customer);
		IEnumerable<OrderLineItem> GetLineItems(Order order);
		bool SetLineItemQuantity(OrderLineItem lineItem, int newQuantity);
		bool RemoveLineItem(OrderLineItem lineItem);
	}

	public interface IProductRepository
	{
		Product GetProductById(int id);
		IEnumerable<Product> GetProductsByLocation(Location location);
	}
}
