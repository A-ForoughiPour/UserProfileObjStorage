using Contract.Dtos.Folder;
using Contract.Services;
using Microsoft.AspNetCore.Mvc;
using MinioApi.Dtos;
using MinioApi.Dtos.Download;
using System.IO;
namespace MinioApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileController:ControllerBase
    {
        private readonly IfileServices _FileServices;

        public FileController(IfileServices FileServices)
        {
            _FileServices = FileServices;
        } 

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPassport([FromForm] UploadUserFileRequest Request)
        {
            try
            {
                FileValidation(Request);

                using var stream = Request.File.OpenReadStream();

                var originalFileName = Path.GetFileNameWithoutExtension(Request.File.FileName);

                var writeUploadFile = new UploadWriteDto(
                    Guid.Parse(Request.UserId.ToString()),
                    "Passport",
                    originalFileName, 
                    Request.File.ContentType,
                    Request.File.Length,
                    stream);
                await _FileServices.UploadUserFile(writeUploadFile);

                return Ok();
            } 
             catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadNationalCard([FromForm] UploadUserFileRequest Request)
        {
            try
            {
                FileValidation(Request);

                using var stream = Request.File.OpenReadStream();

                var writeUploadFile = new UploadWriteDto(
                    Guid.Parse(Request.UserId.ToString()),
                    "NationalCard",
                    Request.File.FileName,
                    Request.File.ContentType,
                    Request.File.Length,
                    stream);

                await _FileServices.UploadUserFile(writeUploadFile);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadBirthCertificate([FromForm] UploadUserFileRequest Request)
        {
            try
            {
                FileValidation(Request);

                using var stream = Request.File.OpenReadStream();

                var writeUploadFile = new UploadWriteDto(
                    Guid.Parse(Request.UserId.ToString()),
                    "BirthCertificate",
                    Request.File.FileName,
                    Request.File.ContentType,
                    Request.File.Length,
                    stream);

                await _FileServices.UploadUserFile(writeUploadFile);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> Download([FromBody] DownloadRequest request)
        {
            var stream = await _FileServices.GetDownloadUrl(request.DownloadWrite);
            if (stream == null) return NotFound();
            var safeFileName = $"{request.DownloadWrite.userId}_{request.DownloadWrite.Path}_{request.DownloadWrite.fileName}.png";
            return File(stream.Content, "image/png", safeFileName);
        }
        [NonAction]
        private ActionResult FileValidation (UploadUserFileRequest Request)
        {
            // Validation with switch expression
            var validationResult = Request switch
            {
                { File: null } or { File.Length: 0 }
                    => BadRequest("No file uploaded."),

                { UserId: var userId } when string.IsNullOrEmpty(userId.ToString())
                    => BadRequest("UserId is required."),

                _ => null // valid case
            };
            return validationResult;

        }
    }
}
