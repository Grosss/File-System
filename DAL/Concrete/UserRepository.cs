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
			User user = context.Set<User>().SingleOrDefault(u => u.UserId == id);
            return user != null 
				? user.ToDalUser() 
				: null;
        }

		public void Create(DalUser entity)
		{
			context.Set<User>().Add(entity.ToOrmUser());
		}

		public void Delete(DalUser entity)
		{
			User user = context.Set<User>().SingleOrDefault(u => u.UserId == entity.Id);
			if (user != null)
			{
				context.Set<User>().Remove(user);
			}
		}

		public void Update(DalUser entity)
		{
			User user = context.Set<User>().SingleOrDefault(u => u.UserId == entity.Id);
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
			User user = context.Set<User>().SingleOrDefault(u =>
				u.Email.ToLower() == email.ToLower());
			return user != null
				? user.ToDalUser()
				: null;
		}

		public DalUser GetUserByLogin(string login)
        {
			User user = context.Set<User>().SingleOrDefault(u =>
				u.Login.ToLower() == login.ToLower());
			return user != null
				? user.ToDalUser()
				: null;
        }
	}
}