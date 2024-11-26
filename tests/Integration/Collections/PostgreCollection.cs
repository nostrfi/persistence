using Nostrfi.Persistence.Integration.Tests.Fixtures;

namespace Nostrfi.Persistence.Integration.Tests.Collections;

[CollectionDefinition(nameof(PostgreCollection))]
public class PostgreCollection : ICollectionFixture<PostgreSqlContainerFixture>;