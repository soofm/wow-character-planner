using CharacterPlanner.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CharacterPlanner.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
