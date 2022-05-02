using Microsoft.AspNetCore.Mvc;
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

			var AudioUrl = await FileHelper.UploadImage(song.Audio);
			song.AudioUrl = AudioUrl;

			await _apiDbContext.Songs.AddAsync(song);
			await _apiDbContext.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created);
		}
	}
}
