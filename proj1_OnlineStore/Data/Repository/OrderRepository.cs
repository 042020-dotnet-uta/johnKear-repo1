using proj1_OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data.Repository
{
	public class OrderRepository : IOrderRepository
	{

		private OnlineStoreDbContext _context;

		public OrderRepository(OnlineStoreDbContext context)
		{
			this._context = context;
		}

		IEnumerable<OrderLineItem> IOrderRepository.GetLineItems(Order order)
		{
			throw new NotImplementedException();
		}

		IEnumerable<Order> IOrderRepository.GetOrderHistory(Customer customer)
		{
			throw new NotImplementedException();
		}

		bool IOrderRepository.RemoveLineItem(OrderLineItem lineItem)
		{
			throw new NotImplementedException();
		}

		bool IOrderRepository.SetLineItemQuantity(OrderLineItem lineItem, int newQuantity)
		{
			throw new NotImplementedException();
		}
	}
}
