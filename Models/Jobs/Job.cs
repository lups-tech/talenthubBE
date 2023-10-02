using System.ComponentModel.DataAnnotations.Schema;
using talenthubBE.Models;

public class Job
{
    [Column("id")]
    public Guid Id {get; set;}
    [Column("jobTech_id")]
    public string? JobTechId {get; set;}
    [Column("url")]
    public String? Url {get; set;}
    [Column("job_text")]
    public String? JobText {get; set;}
    [Column("created_at")]
    public DateTime CreatedAt {get; set;}
    public ICollection<Skill> Skills { get; } = new List<Skill>();
    public ICollection<User> Users { get; } = new List<User>();
    public ICollection<Organization> Organizations { get; } = new List<Organization>();
}
