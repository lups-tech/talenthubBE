using System.ComponentModel.DataAnnotations.Schema;

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
}
