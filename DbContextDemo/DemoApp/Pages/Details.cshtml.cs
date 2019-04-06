using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoApp.Pages
{
	using Data;
	
	public class DetailsModel : PageModel
	{
		public Site Site {get; set;}
		
		public async Task<IActionResult> OnGetAsync(int id, [FromServices] SiteDbContext db)
		{
			Site = await db.GetSiteByIdAsync(id);
			if(Site == null)
				return NotFound();
			return Page();
		}
	}
}

