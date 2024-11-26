using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Nostrfi.Persistence;
[ExcludeFromCodeCoverage]
public static class Initialiser
{
    public static void UseNostrDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NostrContext>();
        context.Migrate();
    }

    public static async Task UseNostrDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NostrContext>();
        await context.MigrateAsync();
    }
}