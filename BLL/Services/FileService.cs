using System;
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
				});
		}
		
		public IEnumerable<FileEntity> GetFilesInSubdirectories(string path, string searchPattern)
		{
			return GetFileList(path, searchPattern)
				.Select(fileName => new FileInfo(fileName))
				.Select(fileInfo => new FileEntity
				{
					Name = fileInfo.Name + " " + fileInfo.FullName,
					FileSizeText = (fileInfo.Length < 1024) ? fileInfo.Length.ToString() + " B" : fileInfo.Length / 1024 + " KB",
					Extension = fileInfo.Extension,
					LastAccessTime = fileInfo.LastAccessTime
				});
		}

		public void DeleteFile(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		#region private methods

		private static IEnumerable<string> GetFileList(string rootFolderPath, string fileSearchPattern)
		{
			Queue<string> pending = new Queue<string>();
			pending.Enqueue(rootFolderPath);
			string[] tmp;
			while (pending.Count > 0)
			{
				rootFolderPath = pending.Dequeue();
				try
				{
					tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
				}
				catch (UnauthorizedAccessException)
				{
					continue;
				}
				foreach (string t in tmp)
				{
					yield return t;
				}
				tmp = Directory.GetDirectories(rootFolderPath);
				foreach (string t in tmp)
				{
					pending.Enqueue(t);
				}
			}
		}

		#endregion
	}
}