using Contract.Dtos.Download;
using Contract.Dtos.Folder;
using Contract.Dtos.Upload;


namespace Contract.Services
{
    public interface IfileServices
    {
         Task<UploadReadDto> UploadUserFile(UploadWriteDto UploadedFile, CancellationToken ct = default);
         Task<DownloadReadDto> GetDownloadUrl(DownloadWriteDto DownloadFilter, CancellationToken ct = default);
         Task DeleteAsync(Guid Id);

    }
}
