using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class Order
	{
		#region Fields
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderId { get; set; }

		[Required(ErrorMessage = "Must have customer id")]
		[ForeignKey("CustomerId")]
		public int CustomerId { get; set; }

		[ForeignKey("LocationId")]
		public int LocationId { get; set; }

		[DataType(DataType.DateTime, ErrorMessage = "Invalid date format")]
		[Required]
		public DateTime Timestamp { get; set; }

		[Required]
		[DisplayName("Order Total")]
		[DataType(DataType.Currency)]
		public double OrderTotal { get; set; }
		#endregion

		#region Constructors
		public Order() { }
		#endregion
	}
}
