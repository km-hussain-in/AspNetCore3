using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
	public class Greeter : Controller
	{		
		public IActionResult Time()
		{
			return View();
		}
		
		public IActionResult Greet(string name, [FromServices] ISet<string> names)
		{
			ViewBag.Visitor = name;
			lock(names)
			{
				names.Add(name);
				ViewBag.Others = from n in names where n != name select n;
			}
			return View();
		}
	}
	
}

