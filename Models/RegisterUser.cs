using System.ComponentModel.DataAnnotations;

namespace proj
{
	public class RegisterUser : basemodel
	{
		[Required]
		[MinLength(3)]
		[RegularExpression(@"^[a-zA-Z]+$")]
		public string Name{get;set;}

		[Required]
		[EmailAddress]
		public string Email{get;set;}

		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string Password{get;set;}

		[Compare("Password", ErrorMessage = "Password and confirmation must match.")]
		[DataType(DataType.Password)]
		[Display(Name="Confirm Password")]
		public string PasswordConfirmation{get;set;}
	}
}
