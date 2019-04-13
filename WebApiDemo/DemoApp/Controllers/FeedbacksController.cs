using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
	using Models;
	
	[ApiController]
	[Route("rest/feedbacks")]
	public class FeedbacksController : ControllerBase
	{
		private AppDbContext _model;
		
		public FeedbacksController(AppDbContext model) => _model = model;
		
		[HttpGet]
		public IEnumerable<Feedback> ReadFeedbacks()
		{
			return _model.Feedbacks.ToList();
		}
		
		[HttpGet("{id}")]
		public ActionResult<Feedback> ReadFeedback(string id)
		{
			Feedback feedback = _model.Feedbacks.Find(id);
			if(feedback == null)
				return NotFound();
			return feedback;
		}
		
		[HttpPost]
		public IActionResult WriteFeedback(Feedback input)
		{
			IActionResult result;
			Feedback feedback = _model.Feedbacks.Find(input.Name);
			if(feedback == null)
			{
				_model.Feedbacks.Add(input);
				result = Created($"/rest/feedbacks/{input.Name}", input);
			}
			else
			{
				feedback.Comment = input.Comment;
				feedback.Rating = input.Rating;
				result = Ok(feedback);
			}
			_model.SaveChanges();
			return result;
		}
	}
	
}

