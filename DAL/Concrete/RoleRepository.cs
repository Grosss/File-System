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
			return context.Set<Role>().FirstOrDefault(r => r.RoleId == id) != null
				? context.Set<Role>().FirstOrDefault(r => r.RoleId == id).ToDalRole()
				: null;
		}

		public void Create(DalRole entity)
		{
			context.Set<Role>().Add(entity.ToOrmRole());
		}

		public void Delete(DalRole entity)
		{
			var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == entity.Id);
			if (role != null)
			{
				context.Set<Role>().Remove(role);
			}
		}

		public void Update(DalRole entity)
		{
			var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == entity.Id);
			if (role != null)
			{
				role.RoleId = entity.Id;
				role.Name = entity.Name;
			}
		}

		public void AddRoleToUser(int userId, int roleId)
		{
			var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == roleId);
			var user = context.Set<User>().FirstOrDefault(u => u.UserId == userId);
			if (user != null && role != null)
			{
				user.Roles.Add(role);
			}
		}

		public void RemoveRoleFromUser(int userId, int roleId)
		{
			var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == roleId);
			var user = context.Set<User>().FirstOrDefault(u => u.UserId == userId);
			if (user != null && role != null)
			{
				user.Roles.Remove(role);
			}
		}

		public IEnumerable<DalRole> GetUserRoles(int userId)
		{
			var user = context.Set<User>().FirstOrDefault(u => u.UserId == userId);
			return user != null 
				? user.Roles.Select(r => r.ToDalRole()) 
				: null;
		}
	}
}