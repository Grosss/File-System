using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ORM;
using System.Data.Entity;
using DAL.Concrete;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM.Entities;

namespace Tests
{
	[TestClass]
	public class NonQueryTests
	{
		//private IQueryable<User> users;

		//[ClassInitialize]
		//public void SetUp()
		//{
		//	users = new List<User>
		//	{
		//		new User {UserId = 1, Email = "qwerty@mail.ru", Login = "qwerty"},
		//		new User {UserId = 2, Email = "admi@gmail.com", Login = "superadmin"},
		//		new User {UserId = 3, Email = "ivanov.ivan@gmail.com", Login = "user007"}
		//	}.AsQueryable();
		//}


		[TestMethod]
		public void GetUserById_ReturnsAUserViaContext()
		{
			var users = new List<User>
			{
				new User {UserId = 1, Email = "qwerty@mail.ru", Login = "qwerty"},
				new User {UserId = 2, Email = "admi@gmail.com", Login = "superadmin"},
				new User {UserId = 3, Email = "ivanov.ivan@gmail.com", Login = "user007"}
			}.AsQueryable();

			var mockSet = new Mock<DbSet<User>>();
			var mockContext = new Mock<FileSystemContext>();
			mockContext.Setup(c => c.Users).Returns(mockSet.Object);
			mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
			mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
			mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
			mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());



			var repository = new UserRepository(mockContext.Object);
			var mockUsers = repository.GetAll().ToList();

			Assert.AreEqual(3, mockUsers.Count);
			Assert.AreEqual("qwerty", mockUsers[0].Login);
			Assert.AreEqual("superadmin", mockUsers[1].Login);
			Assert.AreEqual("ZZZ", mockUsers[2].Login);
		}

		[TestMethod]
		public void GetUserByLogin_ReturnsAUserViaContext()
		{
			Mock<IUserRepository> stub = new Mock<IUserRepository>();

			stub.Setup(ld => ld.GetUserByLogin(It.IsAny<string>()))
				.Returns<DalUser>(user => user);

			var mockUser = "qwerty";
			IUserRepository logger = stub.Object;
			var repository = logger.GetUserByLogin("qwerty");

			Assert.AreEqual(repository, new DalUser());
		}
	}
}
