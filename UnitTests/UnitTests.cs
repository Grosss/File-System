using System.ComponentModel;
using BLL.Interface.Services;
using BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
	[TestClass]
	public class UnitTests
	{
		[TestMethod]
		public void CreateDirectory_PathIsValid_ExpectedPositiveTest()
		{
			//Arrange
			Mock<IDirectoryService> mock = new Mock<IDirectoryService>();

			//Act
			mock.Setup(ds => ds.CreateDirectory(It.IsAny<string>()));

			//Assert
			mock.Verify();
		}

		[TestMethod]
		public void DeleteDirectory_PathIsValid_ExpectedPositiveTest()
		{
			//Arrange
			Mock<IDirectoryService> mock = new Mock<IDirectoryService>();

			//Act
			mock.Setup(ds => ds.DeleteDirectory(It.IsAny<string>()));

			//Assert
			mock.Verify();
		}
	}
}
