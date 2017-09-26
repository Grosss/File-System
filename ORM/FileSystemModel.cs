using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ORM.Entities;

namespace ORM
{
	public class FileSystemContext : DbContext
	{
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Role> Roles { set; get; }

		public FileSystemContext()
			: base("name=FileSystem")
		{
			//Database.SetInitializer<FileSystemContext>(new DropCreateDatabaseIfModelChanges<FileSystemContext>());
			Database.SetInitializer<FileSystemContext>(new AuctionDbInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany<Role>(u => u.Roles)
				.WithMany(r => r.Users)
				.Map(ur =>
				{
					ur.MapLeftKey("UserId");
					ur.MapRightKey("RoleId");
					ur.ToTable("UserRoles");
				});
		}
	}

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