using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Download
{
    public record DownloadWriteDto(Guid userId, string Path, string fileName, int ExpirySecond);
  
}
