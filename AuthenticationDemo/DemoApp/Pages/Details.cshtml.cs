using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DemoApp.Pages
{
	using Data;
	
	public class DetailsModel : PageModel
	{
		private AppDbContext _db;
		
		public DetailsModel(AppDbContext db) => _db = db;
		
		public Visitor Visitor {get; set;}
		
		public void OnGet()
		{
			Visitor = _db.Visitors.Find(HttpContext.User.Identity.Name);
		}
	
		public void OnPost(string spotName)
		{
			Visitor = _db.Visitors.Find(HttpContext.User.Identity.Name);
			Visit visit = Visitor.Visits.FirstOrDefault(entry => entry.Spot == spotName);
			if(visit == null)
				Visitor.Visits.Add(new Visit(spotName, Visitor));	
			else
			{
				visit.Frequency += 1;
				visit.Recent = DateTime.Now;
			}
			_db.SaveChanges(); 
		}	
		
		public async Task<IActionResult> OnGetLogoutAsync()
		{
			await HttpContext.SignOutAsync();
			return RedirectToPage("Index");
		}
	}
}

