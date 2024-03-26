using Nostrfi.Database.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Database.Persistence.Integration.Tests.Collections;

[CollectionDefinition(nameof(PostgreCollection))]
public class PostgreCollection : ICollectionFixture<PostgreSqlContainerFixture>;