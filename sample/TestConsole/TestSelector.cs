using Microsoft.AspNetCore.Builder;
using Nostrfi.Tests;
using Spectre.Console;

namespace Nostrfi;

public static class TestSelector
{
    public static async Task Run(WebApplication application)
    {
        AnsiConsole.Write(
            new FigletText(FigletFont.Default, "Nostrfi Persistence Tests")
                .Centered()
                .Color(Color.Purple));

        var testToRun = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Test [/] to run")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more test)[/]")
                .AddChoices("Add Events"));


        switch (testToRun)
        {
            case "Add Events":
                await AddEventTest.Run(application);
                break;
        }

        await application.StopAsync();
    }
}