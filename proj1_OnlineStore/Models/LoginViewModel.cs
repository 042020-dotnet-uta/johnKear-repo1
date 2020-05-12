using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "User name is required.")]
		[DisplayName("User Name")]
		[MaxLength(20, ErrorMessage = "User name can only be 20 characters.")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[Range(5, 30, ErrorMessage = "{0} must be between {1} and {2}.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
