using Microsoft.AspNetCore.Mvc;
using PaperAndProse.Models;
using PaperAndProse.Helpers;
using System.Collections.Generic;
using System.Linq;


namespace PaperAndProse.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult Checkout()
		{
			var cart = HttpContext.Session.GetObject<List<Book>>("Cart") ?? new List<Book>();
			var user = HttpContext.Session.GetObject<Account>("LoggedInUser");
			
			var order = new Order
			{
				CartItems = cart,
				Total = cart.Sum(b => b.Price),
				Name = user?.Name ?? "", //Pre-Fill if logged in
				Email = user?.Email ?? "" //Pre-fill if logged in
			};
			return View(order);
		}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			// Manual check for required fields only
			if (string.IsNullOrWhiteSpace(order.Name) ||
				string.IsNullOrWhiteSpace(order.Email) ||
				string.IsNullOrWhiteSpace(order.Address))
			{
				TempData["Error"] = "Please fill in all required fields.";
				order.CartItems = HttpContext.Session.GetObject<List<Book>>("Cart") ?? new List<Book>();
				order.Total = order.CartItems.Sum(b => b.Price);
				return View(order);
			}

			// Save the order for confirmation
			HttpContext.Session.SetObject("LastOrder", order);
			HttpContext.Session.Remove("Cart");

			return RedirectToAction("Confirmation");
		}


		public IActionResult Confirmation()
		{
			var order = HttpContext.Session.GetObject<Order>("LastOrder");

			if (order == null)
			{
				// In case user accesses confirmation without a session order
				return RedirectToAction("Checkout");
			}

			return View(order);
		}
	}

	}

