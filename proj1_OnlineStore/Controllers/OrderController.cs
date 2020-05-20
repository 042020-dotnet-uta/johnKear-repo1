using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proj1_OnlineStore.Data;
using proj1_OnlineStore.Models;

namespace proj1_OnlineStore.Controllers
{
	public class OrderController : Controller
	{
		OnlineStoreDbContext _context;
		IProductRepository<Product> _productRepository;
		ILocationRepository<Location> _locationRepository;

		public OrderController(
			OnlineStoreDbContext context, 
			IProductRepository<Product> productRepository,
			ILocationRepository<Location> locationRepository
			)
		{
			this._context = context;
			this._productRepository = productRepository;
			this._locationRepository = locationRepository;
		}

		
		[AllowAnonymous]
		public async Task<IActionResult> Products(string locationName)
		{
			//var locations = await _locationRepository.GetLocations();
			//IQueryable<string> locations = await _locationRepository.GetQueryLocations();
			//IQueryable<string> locNames = (IQueryable<string>)(from l in locations orderby l.LocationName select l.LocationName);
			var locations = await _locationRepository.GetLocations();

			var products = from p in _context.Products select p; //  get all products
			var listProducts = new List<Product>();
			if (!String.IsNullOrEmpty(locationName))
			{
				var search = locations.Where(l => l.LocationName == locationName).Select(c => c.LocationId).FirstOrDefault();
				listProducts = await products.Where(p => p.LocationId == search).ToListAsync();
			}

			var locnames = locations.Select(m => m.LocationName).ToList();

			var productVM = new ProductsViewModel
			{
				Locations = new SelectList(locnames.Distinct().ToList()),
				Products = listProducts
			};

			return View(productVM);
			//return View(await _context.Products.ToListAsync());
		}
		
		[HttpPost]
		[AllowAnonymous]
		public string Products(string locationName, bool notUsed)
		{
			return $"From [HttpPost] index action method: filtered on the substring, {locationName}";
		}


		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var location = await _context.Locations
				.FirstOrDefaultAsync(m => m.LocationId == id);
			if (location == null)
			{
				return NotFound();
			}

			return View(location);
		}

	}
}
