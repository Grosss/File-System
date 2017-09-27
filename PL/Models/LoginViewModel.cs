using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "The field can not be empty!")]
		[Display(Name = "Login")]
		public string Login { get; set; }

		[Required(ErrorMessage = "The field can not be empty!")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}