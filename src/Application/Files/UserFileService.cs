using Contract.Dtos.Download;
using Contract.Dtos.Folder;
using Contract.Dtos.Upload;
using Contract.Repos;
using Contract.Services;

namespace Application.Files;

public class UserFileService : IfileServices
{
    private readonly IFileRepo _repo;

    public UserFileService(IFileRepo fileRepo)
    {
        _repo = fileRepo;
    }
    public async Task DeleteAsync(Guid Id)
    {
        await _repo.DeleteAsync(Id);
    }

    public async Task<DownloadReadDto> GetDownloadUrl(DownloadWriteDto DownloadFilter,CancellationToken ct = default)
    {
        return await _repo.GetDownloadUrl(DownloadFilter, ct);

    }

    public async Task<UploadReadDto> UploadUserFile(UploadWriteDto Upload, CancellationToken ct = default)
    {
        return await _repo.UploadUserFile(Upload, ct); 
    }
}
