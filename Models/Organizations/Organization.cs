using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models
{
    public class Organization
    {
        [Column("id")]
        public required String Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        public ICollection<User> Users { get; } = new List<User>();
        public ICollection<Developer> Developers { get; } = new List<Developer>();
        public ICollection<Job> Jobs { get; } = new List<Job>();
    }
}