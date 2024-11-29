using System.Text.Json;
using System.Text.Json.Serialization;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
       _context= _app.Services.GetRequiredService<NostrContext>();

        var testToRun = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which [green] Availability endpoint [/] to run")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more test)[/]")
                .AddChoices("Add"));

        switch (testToRun)
        {
            case "Add":
                await RunGet();
                break;

        }
    }

    private static async Task RunGet()
        {
            var dbEvent = NostrEvents;
        
           await  _context.Set<Events>().AddAsync(dbEvent);
            await _context.SaveChangesAsync(default);

            AnsiConsole.Write(
                new Panel(new JsonText(JsonSerializer.Serialize(dbEvent, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, // This line handles object references
                        WriteIndented = true, // Optional: makes the output more readable,
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    })))
                    .Header("Add event")
                    .Collapse()
                    .RoundedBorder()
                    .BorderColor(Color.Blue));
        }
    
    private static Persistence.Entities.Events NostrEvents => Builder<Persistence.Entities.Events>.CreateNew()
        .With(x => x.Id = "4496c65d2f232afbe9b882a35baa4f6fe8667c4e684749af565f981833ed6a65")
        .With(x => x.PubKey = "6e468422dfb74a5738702a8823b9b28168abab8655faacb6853cd0ee15deee93")
        .With(x => x.CreatedAt = DateTimeOffset.UtcNow)
        .With(x => x.KindId = 1)
        .With(x => x.Content =
            "Walled gardens became prisons, and nostr is the first step towards tearing down the prison walls.")
        .With(x => x.Sig =
            "908a15e46fb4d8675bab026fc230a0e3542bfade63da02d542fb78b2a8513fcd0092619a2c8c1221e581946e0191f2af505dfdf8657a414dbca329186f009262")
        .With( x => x.Tags = Tags)

        .Build();

    private static List<Tags> Tags =>
    [
        new()
        {
            Identifier = "e",
            Data = ["3da979448d9ba263864c4d6f14984c423a3838364ec255f03c7904b1ae77f206", "52"]
        }

    ];
    }
