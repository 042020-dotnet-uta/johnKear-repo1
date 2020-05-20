using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class ProductRepository : IProductRepository<Product>, IDisposable
	{

		private readonly OnlineStoreDbContext _context;
		private readonly ILogger<Product> _logger;

		public ProductRepository(OnlineStoreDbContext context, ILogger<Product> logger)
		{
			this._context = context;
			this._logger = logger;
		}

		public async Task<bool> DecrementOnLineCreate(int locationId, OrderLineItem lineItem)
		{
			bool success = true;
			// var detail = await _context.LineItems.Where(o => o.OrderId == order.OrderId).ToListAsync();
			var products = await _context.Products.Where(p => p.LocationId == locationId).Select(c=>c).ToListAsync();

			//  for every line item decrement the corresponding product quantity
			/*foreach(var item in lineItem.)
			{
				var update = products.Where(p => p.ProductId == item.ProductId).FirstOrDefault();
				update.Qty -= item.Qty;
			}*/
			foreach(var item in products)
			{
				if (item.ProductId == lineItem.ProductId)
				{
					item.Qty -= lineItem.Qty;
				}
			}
			
			await _context.SaveChangesAsync();
			return success;
		}

		public Task<Product> GetProduct(int productId, Location location)
		{
			throw new NotImplementedException();
		}

		public async Task<Product> GetProductById(int id)
		{
			return await _context.Products.Where(p => p.ProductId == id).Select(c => c).FirstOrDefaultAsync();
		}

		public Task<IEnumerable<Product>> GetProductsByLocation(Location location)
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
