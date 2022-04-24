using Microsoft.EntityFrameworkCore;
using MusicApp.Model;

namespace MusicApp.Data
{
	public class ApiDbContext:DbContext
	{
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)        
        {

        }
        
        public DbSet<Song>? Songs { get; set; }
	}
}
