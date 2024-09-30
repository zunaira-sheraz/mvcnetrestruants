using Microsoft.AspNetCore.Mvc;
using projectmvcrestruants.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace projectmvcrestruants.Controllers
{
	public class AccountController : Controller
	{
		private readonly AccountContext _context; // Your DbContext

		// Constructor to inject the DbContext
		public AccountController(AccountContext context)
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
		public async Task<IActionResult> SignUp(Account model)
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

		// GET: Login View
		public IActionResult SignIn()
		{
			return View();
		}

		// POST: Handle Login
		[HttpPost]
		public IActionResult SignIn(Account model)
		{
			if (!ModelState.IsValid)
			{
				// Check if the user exists in the database
				var user = _context.Accounts.FirstOrDefault(u => u.email == model.email && u.password == model.password);

				if (user != null)
				{
					// Successful login - store the user in session
					HttpContext.Session.SetString("UserName", user.UserName);
					HttpContext.Session.SetString("Email", user.email);

					// Redirect to the Home page after successful login
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// Failed login - add a validation error
					ModelState.AddModelError("", "Invalid username or password");
				}
			}

			// If validation fails, redisplay the form with validation errors
			return View(model);
		}

		// LOGOUT: Clear the session
		public IActionResult Logout()
		{
			// Clear the session
			HttpContext.Session.Clear();

			// Disable browser cache for this page so users can't go back after logging out
			Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
			Response.Headers["Pragma"] = "no-cache";
			Response.Headers["Expires"] = "0";

			// Redirect to SignIn page
			return RedirectToAction("SignIn", "Account");
		}

		public IActionResult AccountDetails()
		{
			// Check if the session exists
			var username = HttpContext.Session.GetString("UserName");

			if (string.IsNullOrEmpty(username))
			{
				// If no session, redirect to SignUp
				return RedirectToAction("SignUp", "Account");
			}

			// Fetch account details if session exists
			var account = _context.Accounts.FirstOrDefault(a => a.UserName == username);
			if (account == null)
			{
				return NotFound();
			}

			return View(account);
		}

		// GET: Edit Account

		public IActionResult EditAccountDetails()
		{
			var username = HttpContext.Session.GetString("UserName"); // Assuming session stores username
			var account = _context.Accounts.FirstOrDefault(a => a.UserName == username);
			if (account == null)
			{
				return NotFound();

			}

			return View(account);
		}

		// POST: Edit Account
		[HttpPost]

		[HttpPost]
		public async Task<IActionResult> EditAccountDetails(Account model)
		{
			if (!ModelState.IsValid)
			{
				var account = _context.Accounts.FirstOrDefault(a => a.id == model.id);
				if (account == null)
				{
					return NotFound();
				}

				// Update account details
				account.UserName = model.UserName;
				account.email = model.email;
				account.dob = model.dob;

				// Save changes
				_context.Update(account);
				await _context.SaveChangesAsync();

				// Update session details with new values
				HttpContext.Session.SetString("UserName", account.UserName);
				HttpContext.Session.SetString("Email", account.email);

				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}

		// GET: Delete Account
		public IActionResult DeleteConfirmation()
		{
			var username = HttpContext.Session.GetString("UserName"); // Assuming session stores username
			var account = _context.Accounts.FirstOrDefault(a => a.UserName == username);
			if (account == null)
			{
				return NotFound();
			}
			return View(account);
		}

		// POST: Delete Account
		[HttpPost]
		public async Task<IActionResult> DeleteAccount(int id)
		{
			var account = _context.Accounts.Find(id);
			if (account == null)
			{
				// If account doesn't exist, show error or redirect to home
				return RedirectToAction("Index", "Home");
			}

			// Delete the account
			_context.Accounts.Remove(account);
			_context.SaveChanges();

			// Log out the user after deleting the account
			HttpContext.Session.Clear(); // Clear the session

			// Redirect to SignIn page after account deletion
			return RedirectToAction("SignUp", "Account");// Redirect to login page after account deletion
		}
	}
}


