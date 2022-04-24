using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Model;

namespace MusicApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SongsController : ControllerBase
	{
		List<Song> songs = new List<Song>() {

			new Song{
			ID = 1,
			Language = "en",
			Title = "My New Song"
		},new Song {
		ID = 2,
			Language = "ur",
			Title = "Mera New Song"
		} 
		
		};

		[HttpGet]
		public IEnumerable<Song> Get()
		{
			return this.songs;
		}
	}	
}
