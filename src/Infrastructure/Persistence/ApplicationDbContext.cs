// using CharacterPlanner.Application.Common.Interfaces;
// using Duende.IdentityServer.EntityFramework.Options;
// using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Options;

// namespace CharacterPlanner.Infrastructure.Persistence;

// public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
// {
//     public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
//     {
        
//     }

//     public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
//     {
//         throw new NotImplementedException();
//     }
// }
