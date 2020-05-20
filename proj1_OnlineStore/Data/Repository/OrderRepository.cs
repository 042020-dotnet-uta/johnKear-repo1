using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public enum AddLineItemResult
	{
		Success,
		ExceedsStock,
		InvalidProduct,
		InvalidOrder
	}

	public class OrderRepository : IOrderRepository<Order>, IDisposable
	{

		private readonly OnlineStoreDbContext _context;
		private readonly ILogger<Order> _logger;
		private readonly IProductRepository<Product> _productRepository;

		public OrderRepository(OnlineStoreDbContext context, ILogger<Order> logger, IProductRepository<Product> productRepository)
		{
			this._context = context;
			this._logger = logger;
			this._productRepository = productRepository;
		}

		public async Task<Order> AddOrder(int customerId, int locationId, double orderTotal)
		{
			_logger.LogInformation("Creating new order for customer: {0}", customerId);
			var newOrder = new Order
			{
				CustomerId = customerId,
				LocationId = locationId,
				OrderTotal = orderTotal,
				Timestamp = DateTime.Now
			};
			_context.Orders.Add(newOrder);

			await _context.SaveChangesAsync();
			var order = await _context.Orders.Where(o => o.Timestamp == newOrder.Timestamp && o.CustomerId == newOrder.CustomerId).FirstOrDefaultAsync();
			return order;

		}

		public async Task<bool> AddLineItem(int customerId, Order order, Product product, int quantity)
		{
			bool success = true;
			var lineItem = new OrderLineItem
			{
				OrderId = order.OrderId,
				ProductId = product.ProductId,
				ProductName = product.ProductName,
				Qty = quantity,
				UnitPrice = product.UnitPrice
			};

			await _context.LineItems.AddAsync(lineItem);

			await _productRepository.DecrementOnLineCreate(order.LocationId, lineItem);

			await _context.SaveChangesAsync();

			return success;
		}

		public async Task<IEnumerable<OrderLineItem>> GetLineItems(Order order)
		{
			var lineItems = await _context.LineItems.Where(o => o.OrderId == order.OrderId).ToListAsync();
			return lineItems;
			
		}

		public Task<IEnumerable<Order>> GetOrderHistory(Customer customer)
		{
			throw new NotImplementedException();
		}

		public Task<Order> RemoveLineItem(OrderLineItem lineItem)
		{
			throw new NotImplementedException();
		}

		public Task<Order> SetLineItemQuantity(OrderLineItem lineItem, int newQuantity)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// implements the IDisposable
		/// </summary>
		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
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
