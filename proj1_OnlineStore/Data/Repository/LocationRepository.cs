using Microsoft.EntityFrameworkCore;
using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class LocationRepository : ILocationRepository<Location>, IDisposable 
	{
		private OnlineStoreDbContext _context;

		public LocationRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		public async Task<IEnumerable<Product>> GetInventory(Location location)
		{
			var inventory = await _context.Products.Where(p => p.LocationId == location.LocationId).ToListAsync();
			return inventory;
		}

		public Task<Location> GetLocations()
		{
			throw new NotImplementedException();
		}

		public async Task<Product> GetProduct(int productId, Location location)
		{
			var product = await _context.Products.Where(p => p.LocationId == location.LocationId)
																.Where(p => p.ProductId == productId)
																.FirstOrDefaultAsync();
			return product;
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
