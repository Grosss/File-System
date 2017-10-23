using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
	public static class BllRoleMapper
	{
		public static RoleEntity ToBllRole(this DalRole role)
		{
			if (role == null)
				return null;

			RoleEntity bllRole = new RoleEntity
			{
				Id = role.Id,
				Name = role.Name,
			};

			return bllRole;
		}

		public static DalRole ToDalRole(this RoleEntity role)
		{
			if (role == null)
				return null;

			DalRole dalRole = new DalRole
			{
				Id = role.Id,
				Name = role.Name
			};

			return dalRole;
		}      
	}
}