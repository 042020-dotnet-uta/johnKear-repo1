﻿using proj1_OnlineStore.Data.Repository;
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
		Task<IEnumerable<Customer>> FindCustomerByFirstName(string fname);
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

	public interface ILocationRepository<T> : IDisposable
	{
		Task<string> GetLocationName(int locationId);
		Task<IEnumerable<Location>> GetLocations();
		Task<IEnumerable<Product>> GetInventory(Location location);
		Task<IQueryable<string>> GetQueryLocations();
		Task<Product> GetProduct(int productId, Location location);
	}
	
	public interface IOrderRepository<T> : IDisposable
	{
		Task<Order> GetOrder(int orderId);
		Task<IEnumerable<Order>> GetOrderHistory(int customerId);
		Task<IEnumerable<OrderLineItem>> GetLineItems(Order order);
		Task<T> SetLineItemQuantity(OrderLineItem lineItem, int newQuantity);
		Task<T> RemoveLineItem(OrderLineItem lineItem);
		Task<bool> AddLineItem(int customerId, Order order, Product product, int quantity);
		Task<Order> AddOrder(int customerId, int locationId, double orderTotal);
	}

	public interface IProductRepository<T> : IDisposable
	{
		Task<T> GetProductById(int id);
		Task<IEnumerable<Product>> GetProductsByLocation(Location location);
		Task<bool> DecrementOnLineCreate(int locationId, OrderLineItem lineItem);
		
	}

	
}
