using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace DemoApp.Pages
{
	using Data;
	
	public class DetailsModel : PageModel
	{
		private AppDbContext _db;
		
		public DetailsModel(AppDbContext db) => _db = db;
		
		public Visitor Visitor {get; set;}
		
		public async void OnGetAsync()
		{
			Visitor = await _db.Visitors.FindAsync(HttpContext.User.Identity.Name);
		}
	
		public async void OnPostAsync(string spotName)
		{
			Visitor = await _db.Visitors.FindAsync(HttpContext.User.Identity.Name);
			Visit visit = Visitor.Visits.FirstOrDefault(entry => entry.Spot == spotName);
			if(visit == null)
				Visitor.Visits.Add(new Visit(spotName, Visitor));	
			else
			{
				visit.Frequency += 1;
				visit.Recent = DateTime.Now;
			}
			await _db.SaveChangesAsync(); 
		}	
		
		public async Task<IActionResult> OnGetLogoutAsync()
		{
			await HttpContext.SignOutAsync();
			return RedirectToPage("Index");
		}
	}
}

