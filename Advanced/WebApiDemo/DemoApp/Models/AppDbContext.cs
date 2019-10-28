using Microsoft.EntityFrameworkCore;

namespace DemoApp.Models
{
	public class AppDbContext : DbContext
	{
		public DbSet<Feedback> Feedbacks {get; set;}
		
		public AppDbContext(DbContextOptions options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}

