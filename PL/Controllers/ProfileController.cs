using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using PL.Infrastrucuture.Mappers;

namespace PL.Controllers
{
	[Authorize]
    public class ProfileController : Controller
    {
	    private readonly IRoleService roleService;
	    private readonly IUserService userService;

		public ProfileController(IRoleService roleService, IUserService userService)
		{
			this.roleService = roleService;
			this.userService = userService;
		}

        public ActionResult MyProfile()
        {
	        var userModel = userService.GetUserByLogin(User.Identity.Name).ToMvcUser(roleService);
            return View(userModel);
        }

		[Authorize(Roles = "admin")]
		public ActionResult GetAllUsers()
		{
			var users = userService.GetAllUsers()
				.OrderBy(u => u.Login)
				.Select(u => u.ToMvcUser(roleService));
			
			if (!users.Any())
				ViewBag.NoUsers = "There are no users";

			return View(users);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
	    public ActionResult UpdateUserRole(int userId, string roleName)
		{
			var user = userService.GetUserById(userId).ToMvcUser(roleService);

			if (user == null)
			{
				return HttpNotFound();
			}

			if (user.Login == User.Identity.Name)
			{
				return View("Error");
			}

			if (user.Roles.Contains(roleName))
			{
				roleService.RemoveRoleFromUser(userId, roleService.GetAllRoles().FirstOrDefault(r => r.Name == roleName).Id);
			}
			else
			{
				roleService.AddRoleToUser(userId, roleService.GetAllRoles().FirstOrDefault(r => r.Name == roleName).Id);
			}

		    return PartialView("_UpdateUserRole", user);
	    }
    }
}