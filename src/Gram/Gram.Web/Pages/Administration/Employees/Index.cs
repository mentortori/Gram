﻿using Gram.Application.Employees.Models;
using Gram.Application.Employees.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration.Employees
{
    [Authorize(Roles = "Administrator, Employee")]
    public class IndexModel : BasePageModel
    {
        public IList<EmployeeListViewModel> Entity { get; set; }

        public async Task OnGet()
        {
            Entity = await Mediator.Send(new GetEmployeesQuery());
        }
    }
}
