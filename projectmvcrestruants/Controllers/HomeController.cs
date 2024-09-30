
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using projectmvcrestruants.Models;
using System.Linq;

namespace projectmvcrestruants.Controllers
{
	public class HomeController : Controller
	{
		private readonly AccountContext _context;

		// Inject the AccountContext in the constructor
		public HomeController(AccountContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Menu()
		{
			return View();
		}
		
	}
}
