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
		
		[HttpGet("{site}")]
		public IEnumerable<Feedback> ReadFeedbacks(string site)
		{
			return _model.Feedbacks.Where(e => e.Site == site).ToList();
		}
		
		[HttpGet("{site}/{name}")]
		public ActionResult<Feedback> ReadFeedback(string site, string name)
		{
			Feedback feedback = _model.Feedbacks.FirstOrDefault(e => e.Site == site && e.Name == name);
			if(feedback == null)
				return NotFound();
			return feedback;
		}
		
		[HttpPost("{site}")]
		public IActionResult WriteFeedback(string site, Feedback input)
		{
			IActionResult result;
			Feedback feedback = _model.Feedbacks.FirstOrDefault(e => e.Site == site && e.Name == input.Name);
			if(feedback == null)
			{
				input.Site = site;
				_model.Feedbacks.Add(input);
				result = Created($"/rest/feedbacks/{site}/{input.Name}", input);
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

