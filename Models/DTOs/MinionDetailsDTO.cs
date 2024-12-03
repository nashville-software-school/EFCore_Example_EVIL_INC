namespace EvilInc.Models.DTOs;

public class MinionDetailsDTO
{
    public string Name { get; set; } = string.Empty;
    public int PowerLevel { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Plots { get; set; } = new();
}