using System.ComponentModel;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using BLL.Services;
using DAL.Interface;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
	[TestClass]
	public class UserServiceTests
	{
		private UserEntity userEntity;
		private Mock<IUnitOfWork> mockUnitOfWork;
		private Mock<IUserRepository> mockUserRepository;

		[TestInitialize]
		public void Initialize()
		{
			userEntity = new UserEntity()
			{
				Id = 11,
				Login = "newuser",
				Email = "newuser@gmail.com",
				Password = "qwe123"
			};

			mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUserRepository = new Mock<IUserRepository>();
		}

		[TestCleanup]
		public void Cleanup()
		{
			userEntity = null;
			mockUnitOfWork = null;
			mockUserRepository = null;
		}

		[TestMethod]
		public void CreateUser_EntityIsNotNull_ExpectedPositiveTest()
		{
			//Arrange
			UserService userService = new UserService(mockUnitOfWork.Object, mockUserRepository.Object);
			mockUserRepository.Setup(ur => ur.Create(It.Is<DalUser>(u => u.Id == 11)));

			//Act
			userService.CreateUser(userEntity);

			//Assert
			mockUserRepository.VerifyAll();
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void GetUserById_IdIsValid_ExpectedPositiveTest()
		{
			//Arrange
			UserEntity userEntity = new UserEntity()
			{
				Id = 11,
				Login = "newuser",
				Email = "newuser@gmail.com",
				Password = "qwe123"
			};

			UserService userService = new UserService(mockUnitOfWork.Object, mockUserRepository.Object);
			mockUserRepository.Setup(ur => ur.GetById(It.Is<int>(id => id == 11)));

			//Act
			userService.GetUserById(userEntity.Id);

			//Assert
			mockUserRepository.VerifyAll();
		}
	}
}
