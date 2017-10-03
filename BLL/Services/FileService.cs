using System.Collections.Generic;
using System.IO;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace BLL.Services
{
	public class FileService : IFileService
	{
		public IEnumerable<FileEntity> GetAllFiles(string path)
		{
			return Directory.EnumerateFiles(path)
				.Select(fileName => new FileInfo(fileName))
				.Select(fileInfo => new FileEntity
				{
					Name = fileInfo.Name,
					FileSizeText = (fileInfo.Length < 1024) ? fileInfo.Length.ToString() + " B" : fileInfo.Length / 1024 + " KB",
					Extension = fileInfo.Extension,
					LastAccessTime = fileInfo.LastAccessTime
				})
				.ToList();
		}

		public FileEntity GetFile(int id)
		{
			throw new System.NotImplementedException();
		}

		public void CreateFile(FileEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteFile(FileEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateFile(FileEntity entity)
		{
			throw new System.NotImplementedException();
		}
	}
}