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
	public class RoleServiceTests
	{
		private RoleEntity roleEntity;
		private Mock<IUnitOfWork> mockUnitOfWork;
		private Mock<IRoleRepository> mockRoleRepository;
		private RoleService roleService;

		[TestInitialize]
		public void Initialize()
		{
			roleEntity = new RoleEntity()
			{
				Id = 4,
				Name = "superadmin"
			};

			mockUnitOfWork = new Mock<IUnitOfWork>();
			mockRoleRepository = new Mock<IRoleRepository>();
			roleService = new RoleService(mockUnitOfWork.Object, mockRoleRepository.Object);
		}

		[TestCleanup]
		public void Cleanup()
		{
			roleEntity = null;
			mockUnitOfWork = null;
			mockRoleRepository = null;
			roleService = null;
		}

		[TestMethod]
		public void CreateRole_EntityIsNotNull_ExpectedPositiveTest()
		{
			//Arrange
			mockRoleRepository.Setup(rr => rr.Create(It.Is<DalRole>(r => r.Id == 4)));

			//Act
			roleService.CreateRole(roleEntity);

			//Assert
			mockRoleRepository.Verify(rr => rr.Create(It.Is<DalRole>(r => r.Id == 4)));
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void GetRoleById_IdIsValid_ExpectedPositiveTest()
		{
			//Arrange
			mockRoleRepository.Setup(rr => rr.GetById(It.Is<int>(id => id == 4)));

			//Act
			roleService.GetRoleById(roleEntity.Id);

			//Assert
			mockRoleRepository.Verify(rr => rr.GetById(It.Is<int>(id => id == 4)), Times.Once);
		}

		[TestMethod]
		public void GetAllRoles_PassNothing_ExpectedPositiveTest()
		{
			//Arrange
			mockRoleRepository.Setup(rr => rr.GetAll());

			//Act
			roleService.GetAllRoles();

			//Assert
			mockRoleRepository.Verify(rr => rr.GetAll(), Times.Once);
		}

		[TestMethod]
		public void DeleteRole_EntityIsNotNull_ExpectedPositiveTest()
		{
			//Arrange
			mockRoleRepository.Setup(rr => rr.Delete(It.Is<DalRole>(r => r.Id == 4)));

			//Act
			roleService.DeleteRole(roleEntity);

			//Assert
			mockRoleRepository.Verify(rr => rr.Delete(It.Is<DalRole>(r => r.Id == 4)));
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void UpdateRole_EntityIsNotNull_ExpectedPositiveTest()
		{
			//Arrange
			mockRoleRepository.Setup(rr => rr.Update(It.Is<DalRole>(r => r.Id == 4)));

			//Act
			roleService.UpdateRole(roleEntity);

			//Assert
			mockRoleRepository.Verify(rr => rr.Update(It.Is<DalRole>(r => r.Id == 4)));
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void AddRoleToUser_RoleIdAndUserIdAreValid_ExpectedPositiveTest()
		{
			//Arrange
			mockRoleRepository.Setup(rr => rr.AddRoleToUser(It.Is<int>(u => u == 10), It.Is<int>(r => r == 4)));

			//Act
			roleService.AddRoleToUser(10, 4);

			//Assert
			mockRoleRepository.Verify(rr => rr.AddRoleToUser(It.Is<int>(u => u == 10), It.Is<int>(r => r == 4)));
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void RemoveRoleFromUser_RoleIdAndUserIdAreValid_ExpectedPositiveTest()
		{
			//Arrange
			mockRoleRepository.Setup(rr => rr.RemoveRoleFromUser(It.Is<int>(u => u == 10), It.Is<int>(r => r == 4)));

			//Act
			roleService.RemoveRoleFromUser(10, 4);

			//Assert
			mockRoleRepository.Verify(rr => rr.RemoveRoleFromUser(It.Is<int>(u => u == 10), It.Is<int>(r => r == 4)));
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
		}

		[TestMethod]
		public void GetUserRoles_UserIdIsValid_ExpectedPositiveTest()
		{
			//Arrange
			mockRoleRepository.Setup(rr => rr.GetUserRoles(It.Is<int>(u => u == 4)));

			//Act
			roleService.GetUserRoles(4);

			//Assert
			mockRoleRepository.Verify(rr => rr.GetUserRoles(It.Is<int>(u => u == 4)), Times.Once);
		}
	}
}
