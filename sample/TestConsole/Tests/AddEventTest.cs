using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nostrfi.Persistence;
using Nostrfi.Persistence.Entities;
using Spectre.Console;
using Spectre.Console.Json;

namespace Nostrfi.Tests;

public static class AddEventTest
{
    private static WebApplication _app = null!;
    private static NostrContext _context = null!;

    public static async Task Run(WebApplication app)
    {
        _app = app;
        _context = _app.Services.GetRequiredService<NostrContext>();

        var testToRun = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which [green] Availability endpoint [/] to run")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more test)[/]")
                .AddChoices("Add Single Event", "Add Multiple Events"));

        switch (testToRun)
        {
            case "Add Single Event":
                await AddSingleEvent();
                break;
            case "Add Multiple Events":
                await AddMultipleEvents();
                break;
        }
    }

    private static JsonSerializerOptions SerializationOptions => new()
    {
        ReferenceHandler = ReferenceHandler.Preserve, // This line handles object references
        WriteIndented = true, // Optional: makes the output more readable,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    private static async Task AddSingleEvent()
    {
        var dbEvent =  new FakeEvents().Generate(1).First();

        await _context.Set<Events>().AddAsync(dbEvent);
        await _context.SaveChangesAsync(default);

        AnsiConsole.Write(
            new Panel(new JsonText(JsonSerializer.Serialize(dbEvent, SerializationOptions)))
                .Header("Add Single event")
                .Collapse()
                .RoundedBorder()
                .BorderColor(Color.Blue));
    }
    private static async Task AddMultipleEvents()
    {
        var dbEvents = new FakeEvents().Generate(100);
        await _context.Set<Events>().AddRangeAsync(dbEvents);
        await _context.SaveChangesAsync(default);

        AnsiConsole.Write(
            new Panel(new JsonText(JsonSerializer.Serialize(dbEvents, SerializationOptions)))
                .Header("Add Multiple events")
                .Collapse()
                .RoundedBorder()
                .BorderColor(Color.Blue));
    }

}