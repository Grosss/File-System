using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
	public interface IFileService
	{
		IEnumerable<FileEntity> GetAllFiles(string path);
		IEnumerable<FileEntity> GetFilesInSubdirectories(string path, string searchPattern);
		void DeleteFile(string path);
	}
}