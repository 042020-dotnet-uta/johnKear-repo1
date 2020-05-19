using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class ProductRepository : IProductRepository<Product>, IDisposable
	{

		private OnlineStoreDbContext _context;

		public ProductRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}


		public Task<Product> GetProduct(int productId, Location location)
		{
			throw new NotImplementedException();
		}

		public Task<Product> GetProductById(int id)
		{
			throw new NotImplementedException();
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
