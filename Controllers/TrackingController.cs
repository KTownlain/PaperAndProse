using Microsoft.AspNetCore.Mvc;
using PaperAndProse.Models;
using PaperAndProse.Helpers;

namespace PaperAndProse.Controllers
{
	public class TrackingController : Controller
	{
		public IActionResult Index()
		{
			var user = HttpContext.Session.GetObject<Account>("LoggedInUser");
			var order = HttpContext.Session.GetObject<Order>("LastOrder");

			if (user == null)
			{
				TempData["Error"] = "You must be logged in to track your order.";
				return RedirectToAction("Create", "Account");
			}

			if (order != null)
			{
				ViewBag.Status = "Your order has been processed and is on its way!";
				return View("Result", order);
			}

			ViewBag.Status = "No recent order found for this account.";
			return View("Result", null);
		}
	}
}