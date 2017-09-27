using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.Repository;

namespace BLL.Services
{
	public class RoleService : IRoleService
	{
		private readonly IUnitOfWork uow;
		private readonly IRoleRepository roleRepository;

		public RoleService(IUnitOfWork uow, IRoleRepository roleRepository)
		{
			this.uow = uow;
			this.roleRepository = roleRepository;
		}

		public IEnumerable<RoleEntity> GetAllRoles()
		{
			return roleRepository.GetAll().Select(role => role.ToBllRole());
		}

		public RoleEntity GetRoleById(int id)
		{
			return roleRepository.GetById(id).ToBllRole();
		}

		public void CreateRole(RoleEntity entity)
		{
			roleRepository.Create(entity.ToDalRole());
			uow.Commit();
		}

		public void DeleteRole(RoleEntity entity)
		{
			roleRepository.Delete(entity.ToDalRole());
			uow.Commit();
		}

		public void UpdateRole(RoleEntity entity)
		{
			roleRepository.Update(entity.ToDalRole());
			uow.Commit();
		}

		public void AddRoleToUser(int userId, int roleId)
		{
			roleRepository.AddRoleToUser(userId, roleId);
			uow.Commit();
		}

		public void RemoveRoleFromUser(int userId, int roleId)
		{
			roleRepository.RemoveRoleFromUser(userId, roleId);
			uow.Commit();
		}

		public IEnumerable<RoleEntity> GetUserRoles(int userId)
		{
			return roleRepository.GetUserRoles(userId).Select(role => role.ToBllRole());
		}
	}
}