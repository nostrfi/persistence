using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nostrfi.Relay.Persistence.Exceptions;
using Threenine.Database.Extensions;

namespace Nostrfi.Relay.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddNostrDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringNames.Nostr);
        
        if (string.IsNullOrEmpty(connectionString))
            throw new NostrDbException(PersistenceErrors.NoConnectionStringDefined);

        if (!connectionString.Validate()) throw new NostrDbException(PersistenceErrors.ConnectionStringsInvalid);

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