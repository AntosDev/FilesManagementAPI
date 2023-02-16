using FilesManagement.Core.Infra.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FilesManagement.Infra.DataAccess.Context
{
    internal class FMDbContext: DbContext
    {
        public DbSet<FileEntity> Files { get; set; }

        protected readonly IConfiguration Configuration;

        public FMDbContext()
        {
        }
        public FMDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
            this.Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("MSSQL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
