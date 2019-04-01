using System;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.ViewComponents
{
    public class CopyrightInfo
    {
        public int Begin {get; set;}

        public int End {get; set;}

        public string Owner {get; set;}
    }

    public class CopyrightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int startYear)
        {
            return View(new CopyrightInfo
            {
                Begin = startYear,
                End = DateTime.Now.Year,
                Owner = Environment.MachineName
            });
        }
    }
}
