using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Gram.Application.Interfaces
{
    public interface IAuditContext
    {
        Task DetectChangesAsync(ChangeTracker changeTracker);
        Task AuditAsync();
    }
}
