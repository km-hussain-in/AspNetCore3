using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [Route("/Count")]
    public class CounterController : Controller
    {
        private static ConcurrentDictionary<string, int> counters = new ConcurrentDictionary<string, int>();

        [Route("ForName/{name}")]
        public IActionResult Welcome(string name)
        {
            int count;
            counters.TryGetValue(name, out count);
            counters[name] = ++count;
            var output = new System.Text.StringBuilder();
            output.Append(
            $@"
                <html>
                <head>
                    <title>DemoApp</title>
                </head>
                <body>
                    <h1>Welcome {name}</h1>
                    <p>Number of visits is {count}</p>
                </body>
            ");
            return Content(output.ToString(), "text/html");
        }
    }
}