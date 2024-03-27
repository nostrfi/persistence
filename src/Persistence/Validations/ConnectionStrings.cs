using System.Data.Common;
using Microsoft.IdentityModel.Tokens;
using Nostrfi.Database.Persistence.Exceptions;
using Npgsql;

namespace Nostrfi.Database.Persistence.Validations;

public class ConnectionStrings : Dictionary<string, string>
{
    public ConnectionStrings()
    {
        DbProviderFactories.RegisterFactory(ConnectionStringNames.Nostr, NpgsqlFactory.Instance);
    }

    public bool Validate()
    {
        var errors = new List<Exception>();
        foreach (var (key, value) in this)
            try
            {
                var factory = DbProviderFactories.GetFactory(key);
                using var connection = factory.CreateConnection();
                if (connection == null) throw new NostrDbException(PersistenceErrors.NoConnectionStringDefined);
                connection.ConnectionString = value;
                connection.Open();
            }
            catch (Exception)
            {
                errors.Add(new NostrDbException(PersistenceErrors.NoConnectionStringDefined));
            }

        return errors.IsNullOrEmpty();
    }
}