using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
	public interface IRoleService
	{
		IEnumerable<RoleEntity> GetAllRoles();
		RoleEntity GetRoleById(int id);
		void CreateRole(RoleEntity entity);
		void DeleteRole(RoleEntity entity);
		void UpdateRole(RoleEntity entity);
		void AddRoleToUser(int userId, int roleId);
		void RemoveRoleFromUser(int userId, int roleId);
		IEnumerable<RoleEntity> GetUserRoles(int userId);
	}
}