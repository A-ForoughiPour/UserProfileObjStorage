using Contract.Dtos.Download;
using Contract.Dtos.Folder;
using Contract.Dtos.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Repos
{
    public interface IFileRepo
    {

        Task<UploadReadDto> UploadUserFile(UploadWriteDto UploadedFile, CancellationToken ct = default);
        Task<DownloadReadDto> GetDownloadUrl(DownloadWriteDto DownloadFilter, CancellationToken ct = default);
        Task DeleteAsync(Guid Id);
    }

}
