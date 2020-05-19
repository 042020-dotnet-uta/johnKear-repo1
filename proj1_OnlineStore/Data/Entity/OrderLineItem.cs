using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class OrderLineItem
	{
		#region Fields
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderDetailsId { get; set; }

		[ForeignKey("OrderId")]
		public int OrderId { get; set; }

	
		[ForeignKey("ProductId")]
		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public decimal UnitPrice { get; set; }

		[DisplayName("Quantity")]
		[Required]		
		public int Qty { get; set; }

		#endregion

		#region Constructors
		public OrderLineItem() { }
		#endregion
	}
}
