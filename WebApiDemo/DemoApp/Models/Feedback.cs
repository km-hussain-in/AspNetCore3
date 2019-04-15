using System.ComponentModel.DataAnnotations;

namespace DemoApp.Models
{
	public class Feedback
	{
		public int Id {get; set;}

		public string Site {get; set;}

		[Required]
		public string From {get; set;}
				
		[Range(1, 5)]
		public int Rating {get; set;}

		[StringLength(128)]
		public string Comment {get; set;}

	}
	
}

