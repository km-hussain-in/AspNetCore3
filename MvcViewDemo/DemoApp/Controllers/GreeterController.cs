using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
	[Route("/")]
	public class Greeter : Controller
	{		
		[Route("Time")]
		public IActionResult Clock()
		{
			return View("Time");
		}
		
		[Route("Greet/{name=World}")]
		public IActionResult Greet(string name, [FromServices] ISet<string> names)
		{
			lock(names)
			{
				names.Add(name);
				ViewBag.Others = from n in names where n != name select n;
			}
			return View();
		}

		[Route("Count/{name}")]
		public IActionResult Count(string name)
		{
			return View();
		}

	}
	
}

