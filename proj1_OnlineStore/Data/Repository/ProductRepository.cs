using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class ProductRepository : IProductRepository
	{

		private OnlineStoreDbContext _context;

		public ProductRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		Product IProductRepository.GetProductById(int id)
		{
			throw new NotImplementedException();
		}

		IEnumerable<Product> IProductRepository.GetProductsByLocation(Location location)
		{
			throw new NotImplementedException();
		}
	}
}
