using System.Collections.Generic;
using System.Data.Entity;
using ORM.Entities;

namespace ORM
{
	public class AuctionDbInitializer : DropCreateDatabaseIfModelChanges<FileSystemContext>
	{
		protected override void Seed(FileSystemContext context)
		{
			var defaultRoles = new List<Role>
			{
				new Role() { RoleId = 1, Name = "admin" },
				new Role() { RoleId = 2, Name = "user" }
			};

			foreach (var role in defaultRoles)
				context.Roles.Add(role);

			//var defaultUsers = new List<User>
			//{
			//	new User() { UserId = 1, Email = "superadmin@gmail.com", Login = "superadmin", Password = ""},
			//	new User() { UserId = 2, Email = "simpleadmin@gmail.com", Login = "simpleadmin", Password = ""}
			//};
			
			//foreach (var user in defaultUsers)
			//	context.Users.Add(user);

			base.Seed(context);
		}
	}
}