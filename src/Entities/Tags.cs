

namespace Nostrfi.Persistence.Entities;

public class Tags
{
    public string EventId { get; set; }
    public Events Event { get; set; } = new();
    public string Identifier { get; set; }
    public string[] Data { get; set; } = [];
    
    
}