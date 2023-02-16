using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesManagement.Core.Domain.InvertedDependencies
{
    public interface IFilesRepository
    {
        void Save(IEnumerable<File> files);
        void Delete(IEnumerable<File> files);
    }
}
