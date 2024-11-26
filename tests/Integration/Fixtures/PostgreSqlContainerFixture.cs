using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Nostrfi.Persistence.Integration.Tests.Fixtures;

public class PostgreSqlContainerFixture
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:16.3")
        .WithDatabase("Nostrfi")
        .WithPassword("Password12@")
        .WithUsername("nostrfi")
        .Build();

    public string ConnectionString => _container.GetConnectionString();
    public string ContainerId => _container.Id;

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}