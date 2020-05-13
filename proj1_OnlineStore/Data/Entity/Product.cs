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
		public int LocationId { get; set; }

		[Required]
		[NotNull]
		public string ProductName { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPrice { get; set; }

		/// <summary>
		/// Quantity should never be less than 0
		/// </summary>
		[Range(0, Int64.MaxValue)]
		[DisplayName("Quantity")]
		[Required]
		[NotNull]		
		public int Qty { get; set; }

		#endregion

		#region Constructors
		public Product() { }
		#endregion
	}
}
