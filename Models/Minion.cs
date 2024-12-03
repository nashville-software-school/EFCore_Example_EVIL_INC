using System.ComponentModel.DataAnnotations;

namespace EvilInc.Models;

public class Minion
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public int PowerLevel { get; set; }
    public List<Plot> Plots { get; set; }
}