using Bogus;
using Nostrfi.Persistence.Entities;

namespace Nostrfi.Tests;

public sealed class FakeEvents : Faker<Events>
{
    public FakeEvents()
    {
        var tagFaker = new Faker<Tags>()
            .RuleFor(x => x.Identifier, f => "e")
            .RuleFor(x => x.Data, f => [GenerateHashString(64), f.Internet.DomainName()]);
        UseSeed(1)
            .RuleFor(e => e.Id, f => GenerateHashString(64))
            .RuleFor(x => x.PublicKey, f => GenerateHashString(64))
            .RuleFor(x => x.CreatedAt, f => DateTimeOffset.Now)
            .RuleFor(x => x.KindId, 1)
            .RuleFor(x => x.Content, f => f.Lorem.Paragraph())
            .RuleFor(x => x.Signature, f => GenerateHashString(124))
            .RuleFor(x => x.Tags, f => tagFaker.Generate(1));
    }

    private static string GenerateHexString(int length)
    {
        var faker = new Faker();
        const string hexCharacters = "0123456789abcdef";

        return new string(Enumerable.Range(0, length)
            .Select(_ => hexCharacters[faker.Random.Int(0, hexCharacters.Length - 1)])
            .ToArray());
    }

    private static string GenerateHashString(int length)
    {
        return GenerateHexString(length);
    }
}