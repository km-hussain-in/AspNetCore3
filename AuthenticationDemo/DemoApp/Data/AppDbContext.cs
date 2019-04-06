using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Visitor> Visitors {get; set;}
		
		public DbSet<Visit> Visits {get; set;}
				
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlite("FileName=app.db")
				.UseLazyLoadingProxies();
		}
	}
	
}

