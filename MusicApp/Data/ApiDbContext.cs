using Microsoft.EntityFrameworkCore;
using MusicApp.Model;

namespace MusicApp.Data
{
	public class ApiDbContext : DbContext
	{
		public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
		{

		}

		public DbSet<Song>? Songs { get; set; }
		public DbSet<Album>? Albums { get; set; }
		public DbSet<Artist>? Artists { get; set; }

		//protected override void OnModelCreating(ModelBuilder modelIBuilder)
		//{
		//	modelIBuilder.Entity<Song>().HasData(
		//		new Song
		//		{
		//			ID = 100,
		//			Title = "Default Title",
		//			Language = "Default Language",
		//			Duration = "20 min",
		//			Imageurl=""
		//		}
		//		);
		//}
	}
}
