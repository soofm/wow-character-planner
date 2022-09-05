using CharacterPlanner.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CharacterPlanner.Server.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
