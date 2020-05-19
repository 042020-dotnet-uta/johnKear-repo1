using Microsoft.EntityFrameworkCore;
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

		private OnlineStoreDbContext _context;

		public OrderRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		public async Task<AddLineItemResult> AddLineItem(int customerId, Order order, Product product, int quantity)
		{
			if (order == null) return AddLineItemResult.InvalidOrder;
			if (product == null) return AddLineItemResult.InvalidProduct;

			var inStock = await _context.Products.Where(p => p.ProductId == product.ProductId).Where(l => l.LocationId == order.LocationId)
																																				.SumAsync(q => q.Qty);

			if (quantity > inStock) return AddLineItemResult.ExceedsStock;

			var currLineItem = await _context.LineItems.Where(o => o.ProductId == product.ProductId)
																					.Where(o => o.OrderId == order.OrderId)
																					.Select(o => o)
																					.SingleOrDefaultAsync();

			if(currLineItem == null)
			{
				var newLine = new OrderLineItem
				{
					OrderId = order.OrderId,
					ProductId = product.ProductId,
					Qty = quantity,
					UnitPrice = product.UnitPrice,
					ProductName = product.ProductName
				};
				_context.Add(newLine);
			}
			else
			{
				currLineItem.Qty += quantity;
				if (currLineItem.Qty > inStock)
				{
					return AddLineItemResult.ExceedsStock;
				}
			}

			await _context.SaveChangesAsync();
			return AddLineItemResult.Success;																					
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
