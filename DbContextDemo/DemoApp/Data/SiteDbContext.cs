using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data
{
	[Table("Visitors")]
	public class Visitor
	{
		public string Name {get; set;}
		
		[Column("Spot")]
		public int SiteId {get; set;}
		
		public int Frequency {get; set;}
		
		public DateTime? Recent {get; set;}
	}
	
	public class Site
	{
		public int Id {get; set;}
		
		public string Name {get; set;}
		
		public string Country {get; set;}
		
		public ICollection<Visitor> Visitors {get; set;}
	}
	
	public class SiteDbContext : DbContext
	{
		public DbSet<Site> Sites {get; set;}

		public SiteDbContext(DbContextOptions<SiteDbContext> options) : base(options){}
				
		protected override void OnModelCreating(ModelBuilder model)
		{
			model.Entity<Visitor>()
				.HasKey(e => new {e.Name, e.SiteId});
		}
		
		public Task<List<Site>> GetAllSitesAsync() => Sites.ToListAsync();

		//eager loading of child entities		
		//public Task<Site> GetSiteByIdAsync(int siteId) => Sites.Include(p => p.Visitors).SingleOrDefaultAsync(e => e.Id == siteId);

		//explicit loading of child entities
		public async Task<Site> GetSiteByIdAsync(int siteId)
		{
			Site site = await Sites.FindAsync(siteId);
			if(site != null)
				await Entry(site).Collection(e => e.Visitors).LoadAsync();
			return site;
		}
	}
}

