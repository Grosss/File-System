using System.Collections.Generic;
using System.Data.Entity;
using ORM.Entities;

namespace ORM
{
	public class AuctionDbInitializer : DropCreateDatabaseIfModelChanges<FileSystemContext>
	{
		protected override void Seed(FileSystemContext context)
		{
			List<Role> defaultRoles = new List<Role>
			{
				new Role() { RoleId = 1, Name = "admin" },
				new Role() { RoleId = 2, Name = "user" }
			};

			foreach (Role role in defaultRoles)
				context.Roles.Add(role);

			base.Seed(context);
		}
	}
}