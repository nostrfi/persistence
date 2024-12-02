using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nostrfi.Persistence;
using Nostrfi.Persistence.Entities;
using Spectre.Console;
using Spectre.Console.Json;

namespace Nostrfi.Tests;

public class GetEventTest
{
    private static WebApplication _app = null!;
    private static NostrContext _context = null!;

    private static JsonSerializerOptions SerializationOptions => new()
    {
        ReferenceHandler = ReferenceHandler.Preserve,            // This line handles object references
        WriteIndented = true,                                   // Optional: makes the output more readable,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };
    
    public static async Task Run(WebApplication app)
    {
        _app = app;
        _context = _app.Services.GetRequiredService<NostrContext>();

        var testToRun = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which [green] Availability endpoint [/] to run")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more test)[/]")
                .AddChoices("Get By Kind", "Get By Tag Value"));

        switch (testToRun)
        {
            case "Get By Kind":
                await GetByKind();
                break;
            case "Get By Tag Value":
                await GetByTagValue();
                break;
        }
    }
    
    private static async Task GetByKind()
    {
        var kindId = AnsiConsole.Ask<int>("[blue]Which Kind ID to filter against ?[/]");
        var dbEvents = await _context.Set<Events>().Include(x => x.Tags).Include(x => x.Kind).Where(x => x.KindId.Equals(kindId)).ToListAsync();
  
      
        AnsiConsole.Write(
            new Panel(new JsonText(JsonSerializer.Serialize(dbEvents, SerializationOptions)))
                .Header("Retrieved events")
                .Collapse()
                .RoundedBorder()
                .BorderColor(Color.Blue));
    }
    
    private static async Task GetByTagValue()
    {
        var tagValue = AnsiConsole.Ask<string>("[blue]Tag Value[/]");
        var dbEvents = await _context.Set<Events>().Include(x => x.Tags).Include(x => x.Kind).SingleAsync(x => x.Tags.Any(tag => tag.Data.Contains(tagValue)));
  
      
        AnsiConsole.Write(
            new Panel(new JsonText(JsonSerializer.Serialize(dbEvents, SerializationOptions)))
                .Header("Retrieved events")
                .Collapse()
                .RoundedBorder()
                .BorderColor(Color.Blue));
    }
}