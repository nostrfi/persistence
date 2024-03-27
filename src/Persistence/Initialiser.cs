using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Nostrfi.Database.Persistence;

public static class Initialiser
{
    public static void UseNostrfiDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NostrfiContext>();
        context.Migrate();
    }

    public static async Task UseNostrfiDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NostrfiContext>();
        await context.MigrateAsync();
    }
}