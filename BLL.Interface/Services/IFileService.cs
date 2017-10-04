using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
	public interface IFileService
	{
		IEnumerable<FileEntity> GetAllFiles(string path);
		FileEntity GetFile(int id);
		void CreateFile(FileEntity entity);
		void DeleteFile(string path);
	}
}