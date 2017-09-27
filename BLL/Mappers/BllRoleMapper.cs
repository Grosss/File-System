using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
	public static class BllRoleMapper
	{
		public static RoleEntity ToBllRole(this DalRole role)
		{
			var bllRole = new RoleEntity
			{
				Id = role.Id,
				Name = role.Name,
			};

			return bllRole;
		}

		public static DalRole ToDalRole(this RoleEntity role)
		{
			var dalRole = new DalRole
			{
				Id = role.Id,
				Name = role.Name
			};

			return dalRole;
		}      
	}
}