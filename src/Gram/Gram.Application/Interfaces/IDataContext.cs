using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Interfaces
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Employee> Employees { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<GeneralType> GeneralTypes { get; set; }
        DbSet<Participation> Participations { get; set; }
        DbSet<Person> People { get; set; }
    }
}
