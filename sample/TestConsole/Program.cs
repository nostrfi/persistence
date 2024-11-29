// See https://aka.ms/new-console-template for more information


using Microsoft.AspNetCore.Builder;
using Nostrfi;
using Nostrfi.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNostrDatabase(builder.Configuration);

var app = builder.Build();
await app.UseNostrDatabaseAsync();

await TestSelector.Run(app);

