using System.ComponentModel.DataAnnotations.Schema;

namespace talenthubBE.Models
{
    public class User
    {
    [Column("id")]
    public Guid Id {get; set;}
    [Column("auth0Id")]
    public String Auth0Id { get; set; } = "";
    [Column("name")]
    public String? Name {get; set;}
    [Column("email")]
    public String? Email {get; set;}
    [Column("role")]
    public String? Role {get; set;}
    [Column("created_at")]
    public DateTime CreatedAt {get; set;}
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
    public ICollection<Developer> Developers { get; set; } = new List<Developer>();
    }
}