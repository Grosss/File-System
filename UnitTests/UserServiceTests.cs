using System;
using System.ComponentModel;
using System.Diagnostics;
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
		private UserService userService;

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
			userService = new UserService(mockUnitOfWork.Object, mockUserRepository.Object);
		}

		[TestCleanup]
		public void Cleanup()
		{
			userEntity = null;
			mockUnitOfWork = null;
			mockUserRepository = null;
			userService = null;
		}

		[TestMethod]
		public void CreateUser_EntityIsNotNull_ExpectedPositiveTest()
		{
			//Arrange
			mockUserRepository.Setup(ur => ur.Create(It.Is<DalUser>(u => u.Id == 11)));

			//Act
			userService.CreateUser(userEntity);

			//Assert
			mockUserRepository.Verify(ur => ur.Create(It.Is<DalUser>(u => u.Id == 11)));
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void GetUserById_IdIsValid_ExpectedPositiveTest()
		{
			//Arrange
			mockUserRepository.Setup(ur => ur.GetById(It.Is<int>(id => id == 11)));

			//Act
			userService.GetUserById(userEntity.Id);

			//Assert
			mockUserRepository.Verify(ur => ur.GetById(It.Is<int>(id => id == 11)), Times.Once);
		}

		[TestMethod]
		public void GetAllUsers_PassNothing_ExpectedPositiveTest()
		{
			//Arrange
			mockUserRepository.Setup(ur => ur.GetAll());

			//Act
			userService.GetAllUsers();

			//Assert
			mockUserRepository.Verify(ur => ur.GetAll(), Times.Once);
		}

		[TestMethod]
		public void DeleteUser_EntityIsNotNull_ExpectedPositiveTest()
		{
			//Arrange
			mockUserRepository.Setup(ur => ur.Delete(It.Is<DalUser>(u => u.Id == 11)));

			//Act
			userService.DeleteUser(userEntity);

			//Assert
			mockUserRepository.Verify(ur => ur.Delete(It.Is<DalUser>(u => u.Id == 11)));
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void UpdateUser_EntityIsNotNull_ExpectedPositiveTest()
		{
			//Arrange
			mockUserRepository.Setup(ur => ur.Update(It.Is<DalUser>(u => u.Id == 11)));

			//Act
			userService.UpdateUser(userEntity);

			//Assert
			mockUserRepository.Verify(ur => ur.Update(It.Is<DalUser>(u => u.Id == 11)));
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void GetUserByEmail_EmailIsValid_ExpectedPositiveTest()
		{
			//Arrange
			mockUserRepository.Setup(ur => ur.GetUserByEmail(It.Is<string>(e => e == "newuser@gmail.com")));

			//Act
			userService.GetUserByEmail(userEntity.Email);

			//Assert
			mockUserRepository.Verify(ur => ur.GetUserByEmail(It.Is<string>(e => e == "newuser@gmail.com")), Times.Once);
		}

		[TestMethod]
		public void GetUserByLogin_LoginIsValid_ExpectedPositiveTest()
		{
			//Arrange
			mockUserRepository.Setup(ur => ur.GetUserByLogin(It.Is<string>(l => l == "newuser")));

			//Act
			userService.GetUserByLogin(userEntity.Login);

			//Assert
			mockUserRepository.Verify(ur => ur.GetUserByLogin(It.Is<string>(l => l == "newuser")), Times.Once);
		}
	}
}
