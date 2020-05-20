using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Data;
using proj1_OnlineStore.Models;

namespace proj1_OnlineStore.Controllers
{
	public class OrderController : Controller
	{
		private readonly OnlineStoreDbContext _context;
		private readonly IProductRepository<Product> _productRepository;
		private readonly ILocationRepository<Location> _locationRepository;
		private readonly IOrderRepository<Order> _orderRepository;
		private readonly ILogger<Order> _logger;

		public OrderController(
			OnlineStoreDbContext context, 
			IProductRepository<Product> productRepository,
			ILocationRepository<Location> locationRepository,
			IOrderRepository<Order> orderRepository,
			ILogger<Order> logger
			)
		{
			this._context = context;
			this._productRepository = productRepository;
			this._locationRepository = locationRepository;
			this._orderRepository = orderRepository;
			this._logger = logger;
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
		
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Checkout(OrderViewModel order)
		{
			if (!ModelState.IsValid||order.OrderQuantities==null)
			{
				_logger.LogInformation("OrderViewModel is invalid", order);
				return RedirectToAction(nameof(EmptyOrder));				
			}
			
			int totalItems = 0;
			int index = 0;
			List<int> emptyIndices = new List<int>();
			foreach(var item in order.OrderQuantities)
			{
				totalItems += item;
				if (item == 0)
				{
					emptyIndices.Add(index);
				}
				index++;
			}
			if (totalItems == 0)
			{
				return RedirectToAction(nameof(EmptyOrder));
			}

			
			if (!(emptyIndices == null))
			{
				for (int i = emptyIndices.Count-1; i >= 0; i--)
				{
					order.Products.RemoveAt(emptyIndices[i]);
					order.OrderQuantities.RemoveAt(emptyIndices[i]);
				}
				/*foreach (var item in emptyIndices)
				{
					order.Products.RemoveAt(item);
					order.OrderQuantities.RemoveAt(item);
				}*/
			}

			//  calculate cost
			double orderCost = 0;
			index = 0;
			foreach (var item in order.Products)
			{
				Product product = await _productRepository.GetProductById(item);
				//  calculate order total
				orderCost += (product.UnitPrice * order.OrderQuantities[index]);
				index++;
			}

			int locId = order.LocationIds[0];
			int userId = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "UserId").Value);
			//  Create new order and get order id
			_logger.LogInformation("Attempting to create new order for user: {0}, at time: {1}",userId, DateTime.Now);
			var newOrder = await _orderRepository.AddOrder(userId, locId, orderCost);
			index = 0;
			//  Create line items
			List<OrderLineItem> lineItems = new List<OrderLineItem>();
			foreach(var item in order.Products)
			{
				Product product = await _productRepository.GetProductById(item);
				await _orderRepository.AddLineItem(userId, newOrder, product, order.OrderQuantities[index]);
				index++;
			}

			// var items = await _orderRepository.GetLineItems(newOrder);

			lineItems.AddRange(await _orderRepository.GetLineItems(newOrder));

			/*var productVM = new ProductsViewModel
			{
				Locations = new SelectList(locnames.Distinct().ToList()),
				Products = listProducts
			};*/
			//  get location name
			string locName = await _locationRepository.GetLocationName(locId);
			var checkoutVM = new CompletedOrder
			{
				Date = newOrder.Timestamp,
				Location = locName,
				Total = orderCost,
				OrderLineItems = lineItems
			};

			return View(checkoutVM);
		}

		public IActionResult EmptyOrder()
		{
			return View();
		}

	}
}
