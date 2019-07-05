using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Gram.Infrastructure.Interfaces
{
    public interface IAuditService
    {
        Task DetectChangesAsync(ChangeTracker changeTracker);
        Task AuditAsync();
    }
}
