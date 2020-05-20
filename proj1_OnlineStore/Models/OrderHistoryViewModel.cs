using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{

	public class OrderHistoryViewModel
	{
		public List<Order> Orders { get; set; }
	}
}
