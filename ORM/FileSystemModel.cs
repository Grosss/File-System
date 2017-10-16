using System.Data.Entity;
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
}