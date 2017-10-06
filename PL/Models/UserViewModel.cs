using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
	public class UserViewModel
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}