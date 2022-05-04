using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Model
{
	public class Album
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? ImageUrl { get; set; }
		public int ArtistID { get; set; }
		[NotMapped]
		public IFormFile? image { get; set; }
		public ICollection<Song>? Songs { get; set; }



	}
}
