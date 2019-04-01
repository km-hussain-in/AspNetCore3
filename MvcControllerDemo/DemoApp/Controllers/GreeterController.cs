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
			return Content(DateTime.Now.ToString());
		}
		
		public IActionResult Greet(string name, [FromServices] ISet<string> names)
		{
			IEnumerable<string> others;
			lock(names)
			{
				names.Add(name);
				others = from n in names where n != name select n;
			}
			var output = new System.Text.StringBuilder();
			output.Append(
			$@"
				<html>
				<head>
					<title>DemoApp</title>
				</head>
				<body>
					<h1>Hello {name}</h1>
					<h3>Other Visitors</h3>
					<ul>
			");
			foreach(var item in others)
				output.Append($"<li><a href='/Count/ForName/{item}'>{item}</li>");
			output.Append(
			@"
					</ul>
				</body>
				</html>
			");

			return Content(output.ToString(), "text/html");
		}
	}
	
}

