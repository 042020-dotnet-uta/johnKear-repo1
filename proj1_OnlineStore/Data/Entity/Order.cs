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

		[Required(ErrorMessage = "Must have user id")]
		[ForeignKey("UserId")]
		public virtual User UserId { get; set; }

		private int locationId;
		[ForeignKey("LocationId")]
		public int LocationId
		{
			get { return locationId; }
			set { locationId = value; }
		}

		[Required]
		[DataType(DataType.DateTime, ErrorMessage = "Invalid date format")]
		public DateTime Timestamp { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[DisplayName("Order Total")]
		public float? OrderTotal { get; set; }
		#endregion

		#region Constructors
		public Order() { this.Timestamp = DateTime.Now; }
		#endregion
	}
}
