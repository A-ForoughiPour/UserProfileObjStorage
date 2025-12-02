using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrustructure.PersistanceEf;
public class FileDbContext:DbContext
{
    public FileDbContext(DbContextOptions<FileDbContext> options)
           : base(options)
    {
    }
    public DbSet<UserFile> UsersFile { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FileDbContext).Assembly);
    }
}
