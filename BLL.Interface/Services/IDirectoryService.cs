﻿using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
	public interface IDirectoryService
	{
		IEnumerable<DirectoryEntity> GetAllDirectories(string path);
		DirectoryEntity GetDirectory(int id);
		void CreateDirectory(string path);
		void DeleteDirectory(string path);
	}
}