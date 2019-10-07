using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Interfaces
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Attendance> Attendees { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<EventGuide> EventGuides { get; set; }
        DbSet<EventPartner> EventPartners { get; set; }
        DbSet<GeneralType> GeneralTypes { get; set; }
        DbSet<Guide> Guides { get; set; }
        DbSet<Partner> Partners { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<PersonContactInfo> PersonContactInfos { get; set; }
    }
}
