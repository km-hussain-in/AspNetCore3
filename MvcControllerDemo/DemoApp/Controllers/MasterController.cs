using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
	public class MasterController : ControllerBase
	{		
        private static ConcurrentDictionary<string, int> counters = new ConcurrentDictionary<string, int>();

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
			var page = new StringBuilder();
			page.Append(
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
				page.Append($"<li><a href='/Count/{item}'>{item}</li>");
			page.Append(
			@"
					</ul>
				</body>
				</html>
			");

			return Content(page.ToString(), "text/html");
		}

        public IActionResult Count(string name)
        {
            int count;
            counters.TryGetValue(name, out count);
            counters[name] = ++count;
            var page = new StringBuilder();
            page.Append(
            $@"
                <html>
                <head>
                    <title>DemoApp</title>
                </head>
                <body>
                    <h1>Welcome {name}</h1>
                    <p>Number of visits is {count}</p>
                </body>
				</html>
            ");
            return Content(page.ToString(), "text/html");
        }

	}
}

