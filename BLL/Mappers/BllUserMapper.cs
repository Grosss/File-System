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

			UserEntity bllUser = new UserEntity
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

			DalUser dalUser = new DalUser
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