using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		[HttpGet]
		public async Task<IActionResult> GetAlbums()
		{
			var albums = await (from album in _apiDbContext.Albums
								 select new
								 {
									 ID = album.ID,
									 Name = album.Name,
									 ImageUrl = album.ImageUrl
								 }).ToListAsync();
			return Ok(albums);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAlbunDetails(int albumId)
		{
			var albumdetails = await _apiDbContext.Albums.Where(a => a.ID == albumId).Include(a => a.Songs).ToListAsync();

			return Ok(albumdetails);
		}
	}
}
