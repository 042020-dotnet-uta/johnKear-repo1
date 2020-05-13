using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class Location
	{

		#region Fields
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LocationId { get; set; }

		/// <summary>
		/// Location name is required and may only upper/lowercase letters with a max length of 50
		/// </summary>		
		[DisplayName("Name")]
		[RegularExpression(@"^[a-zA-Z''-'/s{1,50}$")]
		[Required(ErrorMessage = "Location name is required")]
		public string LocationName { get; set; }

		#endregion

		#region Constructors
		public Location() { }
		#endregion
	}
}
