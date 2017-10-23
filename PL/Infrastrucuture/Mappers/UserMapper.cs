using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using PL.Models;

namespace PL.Infrastrucuture.Mappers
{
	public static class UserMapper
	{
		public static UserViewModel ToMvcUser(this UserEntity user, IRoleService roleService)
		{
			UserViewModel mvcUser = new UserViewModel
			{
				Id = user.Id,
				Email = user.Email,
				Login = user.Login,
				Roles = roleService.GetUserRoles(user.Id).Select(r => r.Name)
			};

			return mvcUser;
		}

		public static UserEntity ToBllUser(this UserViewModel user, IUserService userService)
		{
			UserEntity bllUser = new UserEntity
			{
				Id = user.Id,
				Email = user.Email,
				Login = user.Login,
				Password = userService.GetUserById(user.Id).Password
			};

			return bllUser;
		}
	}
}