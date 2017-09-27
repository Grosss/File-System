using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
	public interface IUserService
	{
		IEnumerable<UserEntity> GetAllUsers();
		UserEntity GetUserById(int id);
		void CreateUser(UserEntity entity);
		void DeleteUser(UserEntity entity);
		void UpdateUser(UserEntity entity);
		UserEntity GetUserByEmail(string email);
		UserEntity GetUserByLogin(string login);
	}
}