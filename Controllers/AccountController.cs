using Microsoft.AspNetCore.Mvc;
using PaperAndProse.Models;
using PaperAndProse.Helpers;
using System.Security.Principal;

namespace PaperAndProse.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Account account)
		{
			if (string.IsNullOrWhiteSpace(account.Name) ||
				string.IsNullOrWhiteSpace(account.Email) ||
				string.IsNullOrWhiteSpace(account.Password))
			{
				TempData["Error"] = "All fields are required.";
				return View(account);
			}

			HttpContext.Session.SetObject("LoggedInUser", account);
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Remove("LoggedInUser");
			return RedirectToAction("Index", "Home");
		}
	}
}