using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class OrderDetails
	{
		#region Fields
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderDetailsId { get; set; }

		[ForeignKey("OrderId")]
		public virtual Order OrderId { get; set; }

	
		[ForeignKey("ProductId")]
		public virtual Product ProductId { get; set; }

		[Required]
		[DisplayName("Quantity")]
		public int Qty { get; set; }

		#endregion

		#region Constructors
		public OrderDetails() { }
		#endregion
	}
}
