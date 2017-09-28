using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
	public static class BllUserMapper
	{
		public static UserEntity ToBllUser(this DalUser user)
		{
			if (user == null)
				return null;

			var bllUser = new UserEntity
			{
				Id = user.Id,
				Email = user.Email,
				Login = user.Login,
				Password = user.Password
			};

			return bllUser;
		}

		public static DalUser ToDalUser(this UserEntity user)
		{
			if (user == null)
				return null;

			var dalUser = new DalUser
			{
				Id = user.Id,
				Email = user.Email,
				Login = user.Login,
				Password = user.Password
			};

			return dalUser;
		} 
	}
}