using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class OrderViewModel
	{
		public List<int> Products { get; set; }
		public List<int> LocationIds { get; set; }
		public List<int>	OrderQuantities { get; set; }

	}
}
