using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserFile:BaseEntity
    {
        public UserFile(Guid UserId, string Path, string FileName, string ContentType, long Size)
        {
            this.UserId = UserId;
            this.Path = Path;
            this.fileName = FileName;
            this.contentType = ContentType;
            this.size = Size;
        }
        private UserFile()
        {
            
        }
        public Guid UserId {  get;private set; }
       public string Path {  get;private set; }
       public string fileName { get;private set; }
       public string contentType { get;private set; }
       public long size { get;private set; }

    }
}
