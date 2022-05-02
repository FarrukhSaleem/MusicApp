using Microsoft.AspNetCore.Mvc;
using MusicApp.Data;
using MusicApp.Helpers;
using MusicApp.Model;

namespace MusicApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlbumController : ControllerBase
	{
		private ApiDbContext _apiDbContext;

		public AlbumController(ApiDbContext apiDbContext)
		{
			_apiDbContext = apiDbContext;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] Album album)
		{
			var imageUrl = await FileHelper.UploadImage(album.image);
			album.ImageUrl = imageUrl;
			await _apiDbContext.Albums.AddAsync(album);
			await _apiDbContext.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created);
		}
	}
}
