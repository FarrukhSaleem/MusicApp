using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Data;
using MusicApp.Helpers;
using MusicApp.Model;

namespace MusicApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArtistController : ControllerBase
	{
		private ApiDbContext _apiDbContext;

		public ArtistController(ApiDbContext apiDbContext)
		{
			_apiDbContext = apiDbContext;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] Artist artist)
		{
			var imageUrl = await FileHelper.UploadImage(artist.image);
			artist.ImageUrl = imageUrl;
			await _apiDbContext.Artists.AddAsync(artist);
			await _apiDbContext.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created);
		}
	}
}
