using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
	public static class DalRoleMapper
	{
		public static DalRole ToDalRole(this Role role)
		{
			DalRole dalRole = new DalRole
			{
				Id = role.RoleId,
				Name = role.Name
			};

			return dalRole;
		}

		public static Role ToOrmRole(this DalRole role)
		{
			Role ormRole = new Role
			{
				RoleId = role.Id,
				Name = role.Name,
			};

			return ormRole;
		}
	}
}