using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class LocationRepository : ILocationRepository
	{
		private OnlineStoreDbContext _context;

		public LocationRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		IEnumerable<Product> ILocationRepository.GetInventory(Location location)
		{
			throw new NotImplementedException();
		}

		IEnumerable<Location> ILocationRepository.GetLocations()
		{
			throw new NotImplementedException();
		}
	}
}
