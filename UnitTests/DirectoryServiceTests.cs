using System.ComponentModel;
using BLL.Interface.Services;
using BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
	[TestClass]
	public class DirectoryServiceTests
	{
		private Mock<IDirectoryService> mockDirectory;

		[TestInitialize]
		public void Initialize()
		{
			mockDirectory = new Mock<IDirectoryService>();
		}

		[TestCleanup]
		public void Cleanup()
		{
			mockDirectory = null;
		}  

		[TestMethod]
		public void CreateDirectory_PathIsValid_ExpectedPositiveTest()
		{
			//Arrange
			IDirectoryService directoryService = mockDirectory.Object;

			//Act
			directoryService.CreateDirectory(@"D:\New Folder");

			//Assert
			mockDirectory.Verify(ds => ds.CreateDirectory(It.IsAny<string>()));
		}

		[TestMethod]
		public void DeleteDirectory_PathIsValid_ExpectedPositiveTest()
		{
			//Arrange
			IDirectoryService directoryService = mockDirectory.Object;

			//Act
			directoryService.DeleteDirectory(@"D:\New Folder");

			//Assert
			mockDirectory.Verify(ds => ds.DeleteDirectory(It.IsAny<string>()));
		}
	}
}
