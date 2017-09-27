using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM.Entities;

namespace DAL.Concrete
{
	public class UserRepository : IUserRepository
	{
		private readonly DbContext context;

		public UserRepository(DbContext context)
		{
			this.context = context;
		}

		public IEnumerable<DalUser> GetAll()
		{
			return context.Set<User>().ToList().Select(user => user.ToDalUser());
		}

		public DalUser GetById(int id)
        {
            return context.Set<User>().FirstOrDefault(u => u.UserId == id) != null 
				? context.Set<User>().FirstOrDefault(u => u.UserId == id).ToDalUser() 
				: null;
        }

		public void Create(DalUser entity)
		{
			context.Set<User>().Add(entity.ToOrmUser());
		}

		public void Delete(DalUser entity)
		{
			var user = context.Set<User>().FirstOrDefault(u => u.UserId == entity.Id);
			if (user != null)
			{
				context.Set<User>().Remove(user);
			}
		}

		public void Update(DalUser entity)
		{
			var user = context.Set<User>().FirstOrDefault(u => u.UserId == entity.Id);
			if (user != null)
			{
				user.UserId = entity.Id;
				user.Email = entity.Email;
				user.Login = entity.Login;
				user.Password = entity.Password;
			}
		}

		public DalUser GetUserByEmail(string email)
		{
			var user = context.Set<User>().FirstOrDefault(u =>
				string.Equals(u.Email.ToLower(), email.ToLower(), StringComparison.Ordinal));
			return user != null
				? user.ToDalUser()
				: null;
		}

		public DalUser GetUserByLogin(string login)
        {
			var user = context.Set<User>().FirstOrDefault(u =>
				string.Equals(u.Login.ToLower(), login.ToLower(), StringComparison.Ordinal));
			return user != null
				? user.ToDalUser()
				: null;
        }
	}
}