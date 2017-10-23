using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
	public class DirectoryViewModel
	{
		[Display(Name = "Folder name")]
		[Required(ErrorMessage = "The field can not be empty!")]
		[RegularExpression("^[^\\\\/?*:|\"<>]+$", ErrorMessage = "Invalid folder name")]
		public string Name { get; set; }
		public DateTime LastAccessTime { get; set; }
	}
}