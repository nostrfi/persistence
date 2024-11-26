using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nostrfi.Persistence.Exceptions;

namespace Nostrfi.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddNostrDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringNames.Nostr);
        
        if (string.IsNullOrEmpty(connectionString))
            throw new NostrDbException(PersistenceErrors.NoConnectionStringDefined);
        
        services.AddDbContext<NostrContext>(options =>
        {
            options.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(NostrContext).Assembly.FullName);
                x.SetPostgresVersion(15, 0);
                x.EnableRetryOnFailure(10);
            });
        });
        
        return services;
    }
}