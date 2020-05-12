using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class Product
	{
		#region Fields

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductId { get; set; }

		[ForeignKey("LocationId")]
		public virtual Location LocationId { get; set; }

		[Required]
		[NotNull]
		public string ProductName { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public float? UnitPrice { get; set; }

		/// <summary>
		/// Quantity should never be less than 0
		/// </summary>
		[Required]
		[NotNull]
		[Range(0, Int64.MaxValue)]
		[DisplayName("Quantity")]
		public int Qty { get; set; }

		#endregion

		#region Constructors
		public Product() { }
		#endregion
	}
}
