using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Folder
{
    public record UploadWriteDto(Guid userId, string category, string fileName,string contentType, long size, Stream content);
    
}
