using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PL.Models
{
	public class DirModel
	{
		[Display(Name = "Folder name")]
		[Required(ErrorMessage = "The field can not be empty!")]
		[RegularExpression("^[^\\\\/?%*:|\"<>]+$", ErrorMessage = "Invalid folder name")]
		public string Name { get; set; }
		public DateTime LastAccessTime { get; set; }
	}
}