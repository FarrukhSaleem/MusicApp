using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Helpers;
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
			_apiDbContext = apiDbContext;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSong(int? id)
		{
			var song = await _apiDbContext.Songs.FindAsync(id);
			return Ok(song);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _apiDbContext.Songs.ToListAsync());
		}

		//[HttpPost]
		//public async Task<IActionResult> Create([FromBody] Song song)
		//{
		//	if (song == null)
		//	{
		//		return BadRequest("Please provide the details to add new record");
		//	}
		//	await _apiDbContext.Songs.AddAsync(song);
		//	await _apiDbContext.SaveChangesAsync();
		//	return Ok("New Record added successfully.");
		//}

		[HttpPost]
		public async Task<IActionResult> CreateNewRecordWithFile([FromForm] Song song)
		{			
			var imageUrl = await FileHelper.UploadImage(song.image);
			song.Imageurl = imageUrl;
			await _apiDbContext.Songs.AddAsync(song);
			await _apiDbContext.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int? id,[FromForm] Song song)
		{
			Song? songitem = await _apiDbContext.Songs.FindAsync(id);
			if (songitem != null)
			{
				if (song != null)
				{
					songitem.Title = song.Title;
					songitem.Language = song.Language;
					songitem.Duration = song.Duration;
					if (song.image != null)
					{
						var imageUrl = await FileHelper.UploadImage(song.image);
						songitem.Imageurl = imageUrl;
					}
					await _apiDbContext?.SaveChangesAsync();
					return Ok("Record has been updated successfully");
				}
				else {
					//return BadRequest("Please provide the details to add new record");
					return StatusCode(StatusCodes.Status400BadRequest);
				}
			}
			else {
				return NotFound("Record not found");
			}			
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(int? id)
		{
			if (id == null)
			{
				return BadRequest("Record not found");
			}
			else
			{
				Song? songitem = await _apiDbContext.Songs.FindAsync(id);

				if (songitem == null)
				{
					return BadRequest("Record not found");
				}
				else {
					_apiDbContext?.Remove(songitem);
					await _apiDbContext.SaveChangesAsync();
					return Ok("Record deleted successfully");
				}				
			}
		}
	}	
}