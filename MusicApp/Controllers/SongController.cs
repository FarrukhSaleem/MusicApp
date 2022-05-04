using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Helpers;
using MusicApp.Model;

namespace MusicApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SongController : ControllerBase
	{
		private ApiDbContext _apiDbContext;

		public SongController(ApiDbContext apiDbContext)
		{
			_apiDbContext = apiDbContext;
		}

		[HttpPost]
		public async Task<IActionResult> CreateNewRecordWithFile([FromForm] Song song)
		{
			var imageUrl = await FileHelper.UploadImage(song.image);
			song.ImageUrl = imageUrl;

			var AudioUrl = await FileHelper.UploadFile(song.Audio);
			song.AudioUrl = AudioUrl;

			song.UploadDate = DateTime.Now;
			await _apiDbContext.Songs.AddAsync(song);
			await _apiDbContext.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllSongs()
		{
			var songs = await (from song in _apiDbContext.Songs
								 select new
								 {
									 ID = song.ID,
									 Title = song.Title,
									 Duration = song.Duration,
									 ImageUrl = song.ImageUrl,
									 AudioUrl = song.AudioUrl,
								 }).ToListAsync();
			return Ok(songs);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetFeatureSongs()
		{
			var songs = await (from song in _apiDbContext.Songs
							   where song.IsFeatured== true
							   select new
							   {
								   ID = song.ID,
								   Title = song.Title,
								   Duration = song.Duration,
								   ImageUrl = song.ImageUrl,
								   AudioUrl = song.AudioUrl,
							   }).ToListAsync();
			return Ok(songs);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetNewSongs()
		{
			var songs = await (from song in _apiDbContext.Songs
							   orderby song.UploadDate descending
							   select new
							   {
								   ID = song.ID,
								   Title = song.Title,
								   Duration = song.Duration,
								   ImageUrl = song.ImageUrl,
								   AudioUrl = song.AudioUrl,
							   }).Take(3).ToListAsync();
			return Ok(songs);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> SearchSongs(string	query)
		{
			var songs = await (from song in _apiDbContext.Songs
							   where song.Title.StartsWith(query)
							   select new
							   {
								   ID = song.ID,
								   Title = song.Title,
								   Duration = song.Duration,
								   ImageUrl = song.ImageUrl,
								   AudioUrl = song.AudioUrl,
							   }).Take(3).ToListAsync();
			return Ok(songs);
		}
	}
}
