// See https://aka.ms/new-console-template for more information

using Nostrfi.Relay.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Nostrfi_DB");
builder.Services.AddDbContext<NostrContext>(x => x.UseNpgsql(connectionString));
    


var app = builder.Build();

app.UseNostrDatabase();

