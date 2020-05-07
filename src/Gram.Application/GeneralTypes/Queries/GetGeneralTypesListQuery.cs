using Gram.Application.Abstraction;
using Gram.Application.GeneralTypes.Models;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.GeneralTypes.Queries
{
    public class GetGeneralTypesListQuery : IRequest<List<GeneralTypeListItemModel>>
    {
        private int ParentId { get; }

        public GetGeneralTypesListQuery(int parentId)
        {
            ParentId = parentId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetGeneralTypesListQuery, List<GeneralTypeListItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<GeneralTypeListItemModel>> Handle(GetGeneralTypesListQuery request, CancellationToken cancellationToken)
                => await DataContext.GeneralTypes
                    .Where(m => m.IsListed)
                    .Where(m => m.ParentId == request.ParentId)
                    .Select(m => new GeneralTypeListItemModel
                    {
                        Id = m.Id,
                        Title = m.Title
                    })
                    .OrderBy(m => m.Title)
                    .ToListAsync(cancellationToken);
        }
    }
}
