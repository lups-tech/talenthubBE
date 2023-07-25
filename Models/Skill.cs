using System.ComponentModel.DataAnnotations.Schema;

public class Skill
{
    [Column("id")]
    public Guid Id {get; set;}
    [Column("title")]
    public String? Title {get; set;}
    [Column("type")]
    public String? Type {get; set;}
    [Column("created_at")]
    public DateTime CreatedAt {get; set;}
    public List<Job> Jobs { get; } = new();
    public List<Developer> Developers { get; } = new();
}
