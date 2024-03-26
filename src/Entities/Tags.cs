namespace Nostrfi;

public class Tags
{
    public string Id { get; set; } = string.Empty;
    public string EventId { get; set; }= string.Empty;
    public Events Event { get; set; } = null!;
    public string Tag { get; set; }= string.Empty;
    public string Value { get; set; } = string.Empty;
    
   
}