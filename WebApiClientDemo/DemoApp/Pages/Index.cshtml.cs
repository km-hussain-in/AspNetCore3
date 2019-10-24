using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoApp.Pages
{
	using Models;

	public class IndexModel : PageModel
	{
		private RatingModel _model;

		public IndexModel(RatingModel model) => _model = model;

		[BindProperty]
		public Rating Input {get; set;}

		public string Output {get; set;}

		public void OnGet() => Input = new Rating {Rank = 1};

		public async Task OnPostAsync(string operation)
		{
			if(operation == "Lookup")
			{
            	Rating rating = await _model.ReadRatingAsync(Input.Name);
				if(rating != null)
					Input.Rank = rating.Rank;
				else
					Output = "Not Rated";
			}
			else
			{
				
				int status = await _model.WriteRatingAsync(Input);
				Output = status == 201 ? "Rating Added" : "Rating Changed";
			}
		}
	}
}
