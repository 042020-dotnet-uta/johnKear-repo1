using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class OrderRepository : IOrderRepository<Order>
	{

		private OnlineStoreDbContext _context;

		public OrderRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		public Task<IEnumerable<OrderLineItem>> GetLineItems(Order order)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Order>> GetOrderHistory(Customer customer)
		{
			throw new NotImplementedException();
		}

		public Task<Order> RemoveLineItem(OrderLineItem lineItem)
		{
			throw new NotImplementedException();
		}

		public Task<Order> SetLineItemQuantity(OrderLineItem lineItem, int newQuantity)
		{
			throw new NotImplementedException();
		}
	}
}
