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
		private readonly OnlineStoreDbContext _context;

		public LocationRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		public async Task<IEnumerable<Product>> GetInventory(Location location)
		{
			var inventory = await _context.Products.Where(p => p.LocationId == location.LocationId).ToListAsync();
			return inventory;
		}

		public async Task<IEnumerable<Location>> GetLocations()
		{
			return await _context.Locations.Select(c=>c).ToListAsync();
		}

		public Task<IQueryable<string>> GetQueryLocations()
		{
			IQueryable<string> locations = from loc in _context.Locations orderby loc.LocationName select loc.LocationName;
			return (Task<IQueryable<string>>)locations;
		}

		public async Task<Product> GetProduct(int productId, Location location)
		{
			var product = await _context.Products.Where(p => p.LocationId == location.LocationId)
																.Where(p => p.ProductId == productId)
																.FirstOrDefaultAsync();
			return product;
		}

		public async Task<string> GetLocationName(int locationId)
		{
			return await _context.Locations.Where(l => l.LocationId == locationId).Select(n => n.LocationName).FirstOrDefaultAsync();
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
