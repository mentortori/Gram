using Gram.Application.GeneralTypes.Models;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Application.GeneralTypes.Queries
{
    public class GetAllEventStatusesQueryHandler : IRequestHandler<GetAllEventStatusesQuery, List<GeneralTypeDto>>
    {
        private readonly IDataContext _context;

        public GetAllEventStatusesQueryHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<List<GeneralTypeDto>> Handle(GetAllEventStatusesQuery request, CancellationToken cancellationToken) => await _context.GeneralTypes
            .Where(m => m.ParentId == (int)GeneralTypeParents.EventStatus).Select(m => new GeneralTypeDto
                {
                    Id = m.Id,
                    Title = m.Title
                }).ToListAsync(cancellationToken);
    }
}
