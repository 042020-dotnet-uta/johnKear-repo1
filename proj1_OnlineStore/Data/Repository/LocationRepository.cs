using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class LocationRepository : ILocationRepository<Location>
	{
		private OnlineStoreDbContext _context;

		public LocationRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		public Task<Location> GetInventory(Location location)
		{
			throw new NotImplementedException();
		}

		public Task<Location> GetLocations()
		{
			throw new NotImplementedException();
		}
	}
}
