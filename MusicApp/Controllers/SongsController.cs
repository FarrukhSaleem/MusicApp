using Microsoft.AspNetCore.Mvc;
using MusicApp.Data;
using MusicApp.Model;

namespace MusicApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SongsController : ControllerBase
	{
		private ApiDbContext _apiDbContext;
		public SongsController(ApiDbContext apiDbContext)
		{
			this._apiDbContext = apiDbContext;
		}

		[HttpGet]
		public IEnumerable<Song> Get()
		{
			return _apiDbContext.Songs;
		}

		[HttpPost]
		public void Create([FromBody] Song song)
		{
			_apiDbContext?.Songs?.Add(song);
			_apiDbContext?.SaveChanges();
		}

		[HttpPut]
		public void Update([FromBody] Song song)
		{
			Song? songitem = _apiDbContext?.Songs?.Find(song.ID);
			songitem.Title = song.Title;
			songitem.Language = song.Language;
			_apiDbContext?.SaveChanges();
		}

		[HttpDelete]
		public void Remove([FromBody] Song song)
		{
			Song? songitem = _apiDbContext?.Songs?.Find(song.ID);
			_apiDbContext?.Remove(songitem);
			_apiDbContext?.SaveChanges();
		}
	}	
}