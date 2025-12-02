using Contract.Dtos.Download;
using Contract.Dtos.Folder;
using Contract.Dtos.Upload;
using Contract.Repos;
using Domain.Entities;
using Infrustructure.PersistanceEf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Minio;
using Minio.DataModel.Args;

namespace Infrustructure.PersistanceMinio.Repos
{
    public class FileRepo : IFileRepo
    {
     
        private readonly MinioClient _Minio;
        private readonly ILogger<FileRepo> _logger;
        private readonly FileDbContext _FileDbContext;
        private const string Bucket = "user-files";
        public FileRepo(MinioClient Minio, ILogger<FileRepo> logger,FileDbContext fileDbContext)
        {
            _Minio = Minio;
            this._logger = logger;
            this._FileDbContext = fileDbContext;
        }

        public async Task<UploadReadDto> UploadUserFile( UploadWriteDto UploadedFile, CancellationToken ct = default)
        {
            try
            {
                var FileEntity = new UserFile(UploadedFile.userId,UploadedFile.category,UploadedFile.fileName,UploadedFile.contentType,UploadedFile.size);

                await _Minio.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(Bucket)
                    .WithObject($"{FileEntity.UserId}/{FileEntity.Path}/{FileEntity.fileName}")
                    .WithStreamData(UploadedFile.content)
                    .WithObjectSize(UploadedFile.content.Length)
                    .WithContentType(FileEntity.contentType));
                await _FileDbContext.AddAsync(FileEntity);
                _FileDbContext.SaveChanges();
                return new UploadReadDto(FileEntity.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to upload file {FileName} for user {UserId}",
                    UploadedFile.fileName, UploadedFile.userId);

                throw; // rethrow to let application layer handle it
            }
        }

        public async Task<DownloadReadDto> GetDownloadUrl(DownloadWriteDto DownloadFilter, CancellationToken ct = default)
        {
            var ms = new MemoryStream();
            await _Minio.GetObjectAsync(new GetObjectArgs()
                .WithBucket(Bucket)
                .WithObject($"{DownloadFilter.userId}/{DownloadFilter.Path}/{DownloadFilter.fileName}")
                .WithCallbackStream(stream => stream.CopyTo(ms)));
            ms.Position = 0;
            return new DownloadReadDto(ms);
        }

        public async Task DeleteAsync(Guid Id)
        {
            try
            {
                var File =await _FileDbContext.UsersFile.FirstAsync(u => u.Id == Id);
                File.Remove();
                _FileDbContext.UsersFile.Update(File);
                _FileDbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new ArgumentNullException(ex.ToString());
            }
        }
    }
}
