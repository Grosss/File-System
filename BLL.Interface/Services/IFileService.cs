using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
	public interface IFileService
	{
		IEnumerable<FileEntity> GetAllFiles(string path);
		IEnumerable<FileEntity> GetFileInSubdirectories(string path, string searchPattern);
		FileEntity GetFile(int id);
		void DeleteFile(string path);
	}
}