using Azure.Storage.Blobs;

namespace MusicApp.Helpers
{
	public static class FileHelper
	{
		public static async Task<string> UploadImage(IFormFile file) {
			string ConnectionString = @"DefaultEndpointsProtocol=https;AccountName=musicappaccount;AccountKey=cQASm1a7jsLcjhoadUsW37JXLXmGFEzU3WRIahnelwfyHtkG5VOVqzQRY8LluzneuLucA4vTQ350DCOnzO6X3A==;EndpointSuffix=core.windows.net";
			string containerName = @"musiccover";

			BlobContainerClient blobContainerClient = new BlobContainerClient(ConnectionString, containerName);
			BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
			var memoryStream = new MemoryStream();
			await file.CopyToAsync(memoryStream);
			memoryStream.Position = 0;
			await blobClient.UploadAsync(memoryStream);
			return blobClient.Uri.AbsoluteUri;
		}
	}
}
