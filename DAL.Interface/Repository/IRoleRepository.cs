using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
	public interface IRoleRepository : IRepository<DalRole>
	{
		void AddRoleToUser(int userId, int roleId);
		void RemoveRoleFromUser(int userId, int roleId);
		IEnumerable<DalRole> GetUserRoles(int userId);
	}
}