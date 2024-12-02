using Microsoft.EntityFrameworkCore.Migrations;
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
                x.EnableRetryOnFailure(10);
                x.MigrationsAssembly(typeof(NostrContext).Assembly.FullName);
                x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                    Schema.Name);
            });
        });

        return services;
    }
}