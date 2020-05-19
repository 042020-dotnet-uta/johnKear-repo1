using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class LoginViewModel
	{

		/// <summary>
		/// Login name is required and has a max length of 20 characters
		/// </summary>
		[StringLength(20)]
		[MinLength(5, ErrorMessage = "{0} name must be a minimum length of {1}")]
		[Required(ErrorMessage = "User name is required")]
		public string UserName { get; set; }

		[StringLength(20)]
		[MinLength(8, ErrorMessage = "{0} must be a length of at least {1}")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage ="Password is required")]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }
	}
}
