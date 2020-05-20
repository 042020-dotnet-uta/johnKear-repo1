using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proj1_OnlineStore.Data;
using proj1_OnlineStore.Models;

namespace proj1_OnlineStore.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IOrderRepository<Order> _orderRepo;
		private readonly ICustomerRepository<Customer> _customerRepo;
		private readonly ILogger<Customer> _logger;

		public AccountController(
			IOrderRepository<Order> orderRepository,
			ICustomerRepository<Customer> customerRepository,
			ILogger<Customer> logger
			)
		{
			this._customerRepo = customerRepository;
			this._orderRepo = orderRepository;
			this._logger = logger;
		}

		public IActionResult Account()
		{
			string userName = HttpContext.User.FindFirst(claim => claim.Type == "UserName").Value;
			var accountVM = new AccountViewModel { UserName = userName };
			return View(accountVM);
		}

		public async Task<IActionResult> OrderHistory()
		{
			int userId = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "UserId").Value);
			List<Order> orders = new List<Order>();
				
			var items = await _orderRepo.GetOrderHistory(userId);
			orders.AddRange(items);
			var historyVM = new OrderHistoryViewModel { Orders = orders };
			return View(historyVM);
		}

		// GET: Locations/Details/5
		[HttpGet]
		public async Task<IActionResult> Details(int orderId)
		{
	
			Order newOrder = await _orderRepo.GetOrder(orderId);
			var lineItems = await _orderRepo.GetLineItems(newOrder);
			
			if (lineItems.Count()==0)
			{
				return NotFound();
			}
			
			List<OrderLineItem> list = new List<OrderLineItem>();
			list.AddRange( await _orderRepo.GetLineItems(newOrder));
			var detailVM = new OrderDetailsViewModel { LineItems = list };
			return View(detailVM);
		}
	}
}
