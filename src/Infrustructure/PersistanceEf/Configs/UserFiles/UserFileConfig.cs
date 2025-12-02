
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrustructure.PersistanceEf.Configs.UserFiles
{
    public class UserFileConfig : IEntityTypeConfiguration<UserFile>
    {
        public void Configure(EntityTypeBuilder<UserFile> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f=>f.contentType).IsRequired().HasMaxLength(150);
            builder.Property(f => f.fileName).IsRequired().HasMaxLength(200);
        }
    }
}
