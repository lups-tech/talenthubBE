using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    public ICollection<Developer>? Developers { get; set;}
}
