using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoApp.Pages
{
	using Data;
	
	public class IndexModel : PageModel
	{
		public IEnumerable<Site> Sites {get; set;}
		
		public async void OnGetAsync([FromServices] SiteDbContext db)
		{
			Sites = await db.GetAllSitesAsync();
		}
	}
}

