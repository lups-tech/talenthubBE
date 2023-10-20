using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace talenthubBE.Models
{
    public class User
    {
        [Column("id")]
        public required String Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        public bool IsAdmin {get; set;} = false;
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        public ICollection<Developer> Developers { get; set; } = new List<Developer>();
        public required String OrganizationId { get; set; }
        public required Organization Organization { get; set; } = null!;
    }
}