using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Model
{
	public class Song
	{
		public int ID { get; set; }
		[Required(ErrorMessage ="Please provide the title of the song")]
		public string? Title { get; set; }
		[Required(ErrorMessage = "Please provide the language of the song")]
		public string? Language { get; set; }
		public string? Duration { get; set; }
		public DateTime UploadDate { get; set; }
		public bool IsFeatured { get; set; }
		[NotMapped]
		public IFormFile? image { get; set; }
		public string? ImageUrl { get; set; }

		[NotMapped]
		public IFormFile? Audio { get; set; }
		public string? AudioUrl { get; set; }
		public int ArtistID { get; set; }
		public int? AlbumID { get; set; }
	}
}
