using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoApp.Client.Pages
{
	using Models;

	public class IndexModel : PageModel
	{
		private FeedbackModel _model;

		public IndexModel(FeedbackModel model) => _model = model;

		[BindProperty]
		public Feedback Input {get; set;}

		public string Output {get; set;}

		public void OnGet()
		{
			if(Input == null)
				Input = new Feedback();
		}

		public async Task OnPostAsync(string operation)
		{
			if(operation == "Read")
			{
            	Feedback feedback = await _model.ReadFeedbackAsync(Input.Name);
				Output = feedback?.Comment ?? "Not Submitted";
			}
			else
			{
				int status = await _model.WriteFeedbackAsync(Input);
				Output = status == 201 ? "Added" : "Changed";
			}
		}
	}
}
