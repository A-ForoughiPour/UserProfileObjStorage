namespace MinioApi.Dtos.Download
{
    public record UploadUserFileRequest(Guid UserId, IFormFile File);
   
}
