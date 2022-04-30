using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Model
{
	public class Song
	{
		public int ID { get; set; }
		[Required]
		public string? Title { get; set; }
		[Required]
		public string? Language { get; set; }
		public string? Duration { get; set; }
		[NotMapped]
		public IFormFile? image { get; set; }
		public string? Imageurl { get; set; }
	}
}
