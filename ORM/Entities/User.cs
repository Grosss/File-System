using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Entities
{
	public class User
	{
		public User()
		{
			Roles = new HashSet<Role>();
		}

		public int UserId { get; set; }
		[Required]
		[StringLength(50)]
		public string Email { get; set; }
		[Required]
		[StringLength(50)]
		public string Login { get; set; }
		[Required]
		[StringLength(128)]
		public string Password { get; set; }

		public virtual ICollection<Role> Roles { get; set; }
	}
}
