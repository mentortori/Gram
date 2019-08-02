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
    public class GetDropDownListQuery : IRequest<List<GeneralTypeDropDownItemModel>>
    {
        public int ParentId { get; set; }

        public GetDropDownListQuery(int parentId)
        {
            ParentId = parentId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetDropDownListQuery, List<GeneralTypeDropDownItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<GeneralTypeDropDownItemModel>> Handle(GetDropDownListQuery request, CancellationToken cancellationToken)
                => await DataContext.GeneralTypes
                    .Where(m => m.ParentId == request.ParentId).Select(m => new GeneralTypeDropDownItemModel
                    {
                        Id = m.Id,
                        Title = m.Title
                    })
                    .OrderBy(m => m.Title)
                    .ToListAsync(cancellationToken);
        }
    }
}
