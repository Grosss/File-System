using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM.Entities;

namespace DAL.Concrete
{
	public class RoleRepository : IRoleRepository
	{
		private readonly DbContext context;

		public RoleRepository(DbContext context)
		{
			this.context = context;
		}

		public IEnumerable<DalRole> GetAll()
		{
			return context.Set<Role>().ToList().Select(r => r.ToDalRole());
		}

		public DalRole GetById(int id)
		{
			Role role = context.Set<Role>().SingleOrDefault(r => r.RoleId == id);
			return role != null
				? role.ToDalRole()
				: null;
		}

		public void Create(DalRole entity)
		{
			context.Set<Role>().Add(entity.ToOrmRole());
		}

		public void Delete(DalRole entity)
		{
			Role role = context.Set<Role>().SingleOrDefault(r => r.RoleId == entity.Id);
			if (role != null)
			{
				context.Set<Role>().Remove(role);
			}
		}

		public void Update(DalRole entity)
		{
			Role role = context.Set<Role>().SingleOrDefault(r => r.RoleId == entity.Id);
			if (role != null)
			{
				role.RoleId = entity.Id;
				role.Name = entity.Name;
			}
		}

		public void AddRoleToUser(int userId, int roleId)
		{
			Role role = context.Set<Role>().SingleOrDefault(r => r.RoleId == roleId);
			User user = context.Set<User>().SingleOrDefault(u => u.UserId == userId);
			if (user != null && role != null)
			{
				user.Roles.Add(role);
			}
		}

		public void RemoveRoleFromUser(int userId, int roleId)
		{
			Role role = context.Set<Role>().SingleOrDefault(r => r.RoleId == roleId);
			User user = context.Set<User>().SingleOrDefault(u => u.UserId == userId);
			if (user != null && role != null)
			{
				user.Roles.Remove(role);
			}
		}

		public IEnumerable<DalRole> GetUserRoles(int userId)
		{
			User user = context.Set<User>().SingleOrDefault(u => u.UserId == userId);
			return user != null 
				? user.Roles.Select(r => r.ToDalRole()) 
				: null;
		}
	}
}