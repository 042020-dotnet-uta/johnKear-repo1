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
	
	public class User
	{
		#region Fields
		/// <summary>
		/// By convention is primary key
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }

		/// <summary>
		/// Login name is required and has a max length of 20 characters
		/// </summary>
		[Required]
		[MaxLength(20, ErrorMessage = "Login can only be 20 characters")]
		public string Login { get; set; }

		[Required]
		[Range(5, 30, ErrorMessage = "{0} must be between {1} and {2}.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		/// <summary>
		/// First name is required and may only contain upper/lower case letters with a max length of 50
		/// </summary>
		[Required(ErrorMessage = "First name is required")]
		[DisplayName("First Name")]
		[RegularExpression(@"^[a-zA-Z''-'/s{1,50}$")]
		public string FirstName { get; set; }

		/// <summary>
		/// First name is required and may only contain upper/lower case letters with a max length of 50
		/// </summary>
		[Required(ErrorMessage = "Last name is required")]
		[DisplayName("Last Name")]
		[RegularExpression(@"^[a-zA-Z''-'/s{1,50}$")]
		public string LastName { get; set; }

		/// <summary>
		/// Phone number is required
		/// </summary>
		[Required(ErrorMessage = "Phone number is required")]
		[Phone(ErrorMessage = "Must be in valid phone format")]
		[DisplayName("Phone")]
		public PhoneAttribute PhoneNumber { get; set; }

		[ForeignKey("LocationId")]
		[AllowNull]
		public virtual Location DefaultLocation { get; set; }
		#endregion

		#region Constructors
		public User() { }
		#endregion
	}




}
