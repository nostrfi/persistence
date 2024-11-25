namespace Nostrfi.Relay.Persistence.Entities;

public sealed class Events
{
  
  public string Id { get; set; } = string.Empty;
  public string PublicKey { get; set; } = string.Empty;
  public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
  public int Kind { get; set; }
  public string Content { get; set; } = string.Empty;
  public string Sig { get; set; } = string.Empty;
  public List<string[]> Tags { get; set; } = [];
 
}