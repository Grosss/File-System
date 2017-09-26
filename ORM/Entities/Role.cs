using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Entities
{
	public class Role
	{
		public Role()
		{
			Users = new HashSet<User>();
		}

		public int RoleId { get; set; }
		[Required]
		[StringLength(20)]
		public string Name { get; set; }

		public virtual ICollection<User> Users { get; set; }
	}
}
