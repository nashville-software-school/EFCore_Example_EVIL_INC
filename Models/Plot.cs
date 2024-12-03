using System.ComponentModel.DataAnnotations;

namespace EvilInc.Models;

public class Plot {

    public int Id {get ; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public List<Minion> Minions { get; set; }
}