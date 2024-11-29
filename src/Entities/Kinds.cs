namespace Nostrfi.Persistence.Entities;

public class Kinds
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public ICollection<Events> Events { get; set; }
}