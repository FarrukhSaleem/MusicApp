using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Model
{
	public class Artist
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? Gender { get; set; }

		[NotMapped]
		public IFormFile? image { get; set; }
		public string? ImageUrl { get; set; }

		public ICollection<Album>? Albums { get; set; }
	}
}
