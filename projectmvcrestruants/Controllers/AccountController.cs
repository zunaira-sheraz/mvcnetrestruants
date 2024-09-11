using Microsoft.AspNetCore.Mvc;
using projectmvcrestruants.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authentication;

namespace projectmvcrestruants.Controllers
{
	public class AccountController : Controller
	{
		private readonly Account _context; // Your DbContext

		// Constructor to inject the DbContext
		public AccountController(Account context)
		{
			_context = context;
		}

		// GET: Registration Page
		public IActionResult SignUp()
		{
			return View();
		}

		// POST: Handle Registration
		[HttpPost]
		public async Task<IActionResult> SignUp(AccountContext model)
		{
			if (ModelState.IsValid)
			{
				// Check if the email is already registered
				var existingUser = _context.Accounts.FirstOrDefault(u => u.email == model.email);

				if (existingUser != null)
				{
					// Email is already registered, add an error
					ModelState.AddModelError("", "Email is already registered.");
					return View(model);
				}

				// Add the new user to the database
				_context.Accounts.Add(model);
				await _context.SaveChangesAsync();

				// Redirect to the Login page after successful registration
				return RedirectToAction("SignIn", "Account");
			}

			// If validation fails, redisplay the form
			return View(model);
		}

		//for login view

		public IActionResult SignIn()
		{
			return View();
		}


		// POST: Login Verification
		[HttpPost]
		public IActionResult SignIn(AccountContext model)
		{
			if (!ModelState.IsValid)
			{
				// Check if the user exists in the database
				var user = _context.Accounts.FirstOrDefault(u => u.email == model.email && u.password == model.password);

				if (user != null)
				{
					// Successful login - you might want to set authentication cookies or session here
					// For now, redirect to the Home page
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// Failed login - add a validation error and return to the view
					ModelState.AddModelError("", "Invalid useername or password");
				}
			}

			// If validation fails, redisplay the form with validation errors
			return View(model);
		}

	
		
	}

}

