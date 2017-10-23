using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using PL.Filters;
using PL.Models;
using PL.Providers;

namespace PL.Controllers
{
	[CustomErrorHandler]
    public class AccountController : Controller
	{
		private readonly IUserService userService;
		private readonly IRoleService roleService;

		public AccountController(IUserService userService, IRoleService roleService)
		{
			this.userService = userService;
			this.roleService = roleService;
		}
            
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel loginModel, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(loginModel.Login, loginModel.Password))
				{
					FormsAuthentication.SetAuthCookie(loginModel.Login, loginModel.RememberMe);
					if (Url.IsLocalUrl(returnUrl))
						return Redirect(returnUrl);
					else
						return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Wrong password or login");
				}
			}
			return View(loginModel);
		}

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterViewModel registerModel)
		{
			var anyUserLogin = userService.GetUserByLogin(registerModel.Login);

			if (anyUserLogin != null)
			{
				ModelState.AddModelError("", "User with this login has already registered.");
				return View(registerModel);
			}

			var anyUserEmail = userService.GetUserByEmail(registerModel.Email);

			if (anyUserEmail != null)
			{
				ModelState.AddModelError("", "User with this email has already registered.");
				return View(registerModel);
			}

			if (ModelState.IsValid)
			{
				var membershipUser = ((CustomMembershipProvider)Membership.Provider)
					.CreateUser(registerModel.Email, registerModel.Login, registerModel.Password);

				if (membershipUser != null)
				{
					FormsAuthentication.SetAuthCookie(registerModel.Login, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Error registration.");
				}
			}
			return View(registerModel);
		}

    }
}
