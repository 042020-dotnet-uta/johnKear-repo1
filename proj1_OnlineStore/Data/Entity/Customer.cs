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
		[Range(5, 20, ErrorMessage = "{0} must be between {1} and {2}.")]
		[Required]
		public string Login { get; set; }

		[Range(5, 30, ErrorMessage = "{0} must be between {1} and {2}.")]
		[DataType(DataType.Password)]
		[Required]		
		public string Password { get; set; }

		/// <summary>
		/// First name is required and may only contain upper/lower case letters with a max length of 50
		/// </summary>
		[DisplayName("First Name")]
		[RegularExpression(@"^[a-zA-Z]''-'/s{1,50}$")]
		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; }

		/// <summary>
		/// First name is required and may only contain upper/lower case letters with a max length of 50
		/// </summary>
		[DisplayName("Last Name")]
		[Required(ErrorMessage = "Last name is required")]
		[RegularExpression(@"^[a-zA-Z]''-'/s{1,50}$")]
		public string LastName { get; set; }

		/// <summary>
		/// Phone number is required
		/// </summary>
		[Required(ErrorMessage = "Phone number is required")]
		[DisplayName("Phone")]
		[DataType(DataType.PhoneNumber, ErrorMessage = "Must be valid phone format")]
		public string PhoneNumber { get; set; }

		[ForeignKey("LocationId")]
		[AllowNull]
		public int DefaultLocation { get; set; }
		#endregion

		#region Constructors
		public Customer() { }
		#endregion



	}




}
