using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		[HttpGet]
		public async Task<IActionResult> GetArtists()
		{
			var artists = await (from artist in _apiDbContext.Artists
								 select new
								 {
									 ID = artist.ID,
									 Name = artist.Name,
									 ImageUrl = artist.ImageUrl
								 }).ToListAsync();
			return Ok(artists);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetArtistDetails(int artistId)
		{
			var artistdetails = await _apiDbContext.Artists.Where(a => a.ID == artistId).Include(a => a.Songs).ToListAsync();

			return Ok(artistdetails);
		}
	}
}
