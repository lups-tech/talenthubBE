using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace talenthubBE.Models
{
    public class User
    {
    [Column("auth0Id")]
    public required String Auth0Id { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt {get; set;}
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
    public ICollection<Developer> Developers { get; set; } = new List<Developer>();
    }
}