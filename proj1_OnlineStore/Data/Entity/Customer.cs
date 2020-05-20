using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	
	public class Customer
	{
		#region Fields
		/// <summary>
		/// By convention is primary key
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CustomerId { get; set; }

		/// <summary>
		/// Login name is required and has a max length of 20 characters
		/// </summary>
		[StringLength(20)]
		[MinLength(5, ErrorMessage = "{0} name must be a minimum length of {1}")]
		[Required]
		public string Login { get; set; }

		[StringLength(20)]
		[MinLength(8, ErrorMessage = "{0} must be a length of at least {1}")]
		[DataType(DataType.Password)]
		[Required]		
		public string Password { get; set; }

		/// <summary>
		/// First name is required and may only contain upper/lower case letters with a max length of 50
		/// </summary>
		[DisplayName("First Name")]
		[RegularExpression(@"^[a-zA-Z]{1,25}$")]
		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; }

		/// <summary>
		/// First name is required and may only contain upper/lower case letters with a max length of 50
		/// </summary>
		[DisplayName("Last Name")]
		[Required(ErrorMessage = "Last name is required")]
		[RegularExpression(@"^[a-zA-Z]{1,25}$")]
		public string LastName { get; set; }

		/// <summary>
		/// Phone number is required
		/// </summary>
		[Required(ErrorMessage = "Phone number is required")]
		[DisplayName("Phone")]
		[RegularExpression
			(@"^(\d{3})-(\d{3})-(\d{4})$",
			ErrorMessage = "Must be of form: 123-123-1234")]
		[Phone]
		public string PhoneNumber { get; set; }

		[ForeignKey("LocationId")]
		[Range(0, Int32.MaxValue)]
		public int DefaultLocation { get; set; }
		#endregion

		#region Constructors
		public Customer() { }
		#endregion



	}




}
