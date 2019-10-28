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
	
	public class IndexModel : PageModel
	{
		private AppDbContext _db;

		[BindProperty]
		public Visitor Input {get; set;}

		public IndexModel(AppDbContext db) => _db = db;
		
		public void OnGet() => Input = new Visitor();

		public async Task<IActionResult> OnPostSignInAsync()
		{
			var user = await _db.Visitors.FindAsync(Input.Id);
			if(user == null || user.Password != Input.Password)
			{
				ModelState.AddModelError("Input.Password", "Incorrect Id or Password");
				return Page();
			}
			await AuthenticateAsync(Input.Id);
			return RedirectToPage("Details");
		}
	
		public async Task<IActionResult> OnPostSignUpAsync()
		{
			var user = await _db.Visitors.FindAsync(Input.Id);
			if(user != null)
			{
				ModelState.AddModelError("Input.Id", "Id not available");
				return Page();
			}
			if(Input.Password != Input.ConfirmPassword)
			{
				ModelState.AddModelError("Input.Password", "Passwords don't match");
				return Page();
			}
			_db.Visitors.Add(Input);
			await _db.SaveChangesAsync();
			await AuthenticateAsync(Input.Id);
			return RedirectToPage("Details");
		}

		private Task AuthenticateAsync(string id)
		{
			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
			identity.AddClaim(new Claim(ClaimTypes.Name, id));
			return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(identity));
		}

	}
}

