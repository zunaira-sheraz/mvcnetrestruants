using Microsoft.AspNetCore.Mvc;
using projectmvcrestruants.Models;
using System.Linq;

namespace projectmvcrestruants.Controllers
{
	public class LoginController : Controller
	{
		private readonly Registration _context;  // Your DbContext

		public LoginController(Registration context)
		{
			_context = context;
		}

		// GET: Login Page
		public IActionResult Index()
		{
			return View();
		}


		// POST: Login Verification
		[HttpPost]
		public IActionResult Index(Login model)
		{
			if (!ModelState.IsValid)
			{
				// Check if the user exists in the database
				var user = _context.Logins.FirstOrDefault(u => u.email == model.email && u.password == model.password);

				if (user != null)
				{
					// Successful login - you might want to set authentication cookies or session here
					// For now, redirect to the Home page
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// Failed login - add a validation error and return to the view
					ModelState.AddModelError("", "Invalid email or password");
				}
			}

			// If validation fails, redisplay the form with validation errors
			return View(model);
		}
	}




}
