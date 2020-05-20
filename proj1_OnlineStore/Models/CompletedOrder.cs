using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class CompletedOrder
	{

		public string Location { get; set; }
		public DateTime Date { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		[DataType(DataType.Currency)]
		public double Total { get; set; }

		public List<OrderLineItem> OrderLineItems { get; set; }

	}
}
