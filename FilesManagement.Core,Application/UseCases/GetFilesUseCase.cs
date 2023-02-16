using FilesManagement.Common.Application.InvertedDependencies;
using FilesManagement.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesManagement.Core.Application.UseCases
{
    internal class GetFilesUseCase : IRequestHandler<GetFilesQuery, IEnumerable<FileDomain>>
    {
        ISqlConnectionFactory _sqlConnectionFactory;

        public GetFilesUseCase(ISqlConnectionFactory _sqlConnectionFactory)
        {
            this._sqlConnectionFactory = _sqlConnectionFactory;
        }

        public async Task<IEnumerable<FileDomain>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            return await _sqlConnectionFactory.QueryMany<FileDomain>(connection, string.Format(request.SQL));
        }
    }
}
