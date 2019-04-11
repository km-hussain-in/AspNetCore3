using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientApp.Pages
{
	using Models;

	public class IndexModel : PageModel
	{
		private FeedbackModel _model;

		public IndexModel(FeedbackModel model) => _model = model;

		public IEnumerable<Feedback> Feedbacks {get; set;}

		public async Task OnGetAsync()
		{
            Feedbacks = await _model.ReadFeedbacksAsync();
		}

		public async Task<IActionResult> OnGetChangeAsync(string id, string direction)
		{
			Feedbacks = await _model.ReadFeedbacksAsync();
			var feedback = Feedbacks.FirstOrDefault(e => e.Name == id);
			if(direction == "up" && feedback.Rating < 5)
				feedback.Rating += 1;
			if(direction == "down" && feedback.Rating > 1)
				feedback.Rating -= 1;			
			await _model.WriteFeedbackAsync(feedback);
			return Page();
		}
	}
}
