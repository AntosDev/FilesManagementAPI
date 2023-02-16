using FilesManagement.Core.Domain;
using FilesManagement.Core.Domain.InvertedDependencies;
using FilesManagement.Core.Infra.DataAccess.Entities;
using FilesManagement.Infra.DataAccess.Context;

namespace FilesManagement.Core.Infra.DataAccess
{
    public class FilesRepository : IFilesRepository
    {
        private readonly FMDbContext dbContext;

        public FilesRepository(FMDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Save(IEnumerable<Domain.FileDomain> files)
        {
            if (files == null) throw new ArgumentNullException(nameof(files));
            try
            {
                var fileEntities = files.Select(f => ToPersistance(f));
                this.dbContext.Files.AddRange(fileEntities);
                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        public FileDomain Find(string id)
        {
            var dbEntities = this.dbContext.Files.Where(f => f.FileId == id);

            if (dbEntities == null || dbEntities.Count() == 0) throw new InvalidDataException("Resource not found");

            return dbEntities.Select(u => ToAggregate(u)).First();
        }

        public void Delete(IEnumerable<string> ids)
        {
            var entities = this.dbContext.Files.Where(f => ids.Contains(f.FileId));

            if (entities == null || entities.Count() == 0) return;


            this.dbContext.Files.RemoveRange(entities);
            this.dbContext.SaveChanges();
        }
        private static FileDomain ToAggregate(FileEntity dbEntity)
        {
            if (dbEntity == null) throw new Exception("Mapping for null failed");
            return new FileDomain
            {
                Path = dbEntity.Path,
                Name = dbEntity.Name,
                CreatedDate = dbEntity.CreatedDate,
                Size = dbEntity.Size,
                FileId = dbEntity.FileId,
            };
        }
        private static FileEntity ToPersistance(FileDomain domain)
        {
            if (domain == null) throw new Exception("Mapping for null failed");
            return new FileEntity
            {
                Path = domain.Path,
                Name = domain.Name,
                CreatedDate = domain.CreatedDate,
                Size = domain.Size,
                FileId = domain.FileId,
            };
        }
    }
    
}