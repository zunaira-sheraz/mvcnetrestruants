using Microsoft.AspNetCore.Mvc;
using projectmvcrestruants.Models;
using System.Threading.Tasks;
using System.Linq;

namespace projectmvcrestruants.Controllers
{
	public class RegistrationController : Controller
	{
		private readonly Registration _context; // Your DbContext

		// Constructor to inject the DbContext
		public RegistrationController(Registration context)
		{
			_context = context;
		}

		// GET: Registration Page
		public IActionResult Index()
		{
			return View();
		}

		// POST: Handle Registration
		[HttpPost]
		public async Task<IActionResult> Index(Login model)
		{
			if (ModelState.IsValid)
			{
				// Check if the email is already registered
				var existingUser = _context.Logins.FirstOrDefault(u => u.email == model.email);

				if (existingUser != null)
				{
					// Email is already registered, add an error
					ModelState.AddModelError("", "Email is already registered.");
					return View(model);
				}

				// Add the new user to the database
				_context.Logins.Add(model);
				await _context.SaveChangesAsync();

				// Redirect to the Login page after successful registration
				return RedirectToAction("Index", "Login");
			}

			// If validation fails, redisplay the form
			return View(model);
		}
	}
}
