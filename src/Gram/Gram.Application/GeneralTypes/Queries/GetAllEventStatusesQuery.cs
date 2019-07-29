using Gram.Application.GeneralTypes.Models;
using MediatR;
using System.Collections.Generic;

namespace Gram.Application.GeneralTypes.Queries
{
    public class GetAllEventStatusesQuery : IRequest<List<GeneralTypeDto>>
    {
    }
}
