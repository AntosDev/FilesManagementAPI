using Identity.Infra.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Identity.Infra.DataAccess.Context
{
    public class IdentityDBContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        protected readonly IConfiguration Configuration;

        public IdentityDBContext()
        {
        }
        public IdentityDBContext(IConfiguration configuration)
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
            modelBuilder.Entity<UserEntity>()
            .HasData(
                new UserEntity
                {
                    ID = 1,
                    UserId = Guid.NewGuid().ToString(),
                    FirstName = "Antoine",
                    LastName = "Hachem",
                    Password = "10|Jf2tWxMCxFxb/UPkHnxZqA==|NsYGhfw4NYyiWLsSxrTISNAB1SYmrinumZTbaBT7wms=",
                    Username = "AntoineH"
                }
            );
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
