using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace talenthubBE.Models
{
    public class SkillView
    {
    public Guid Id {get; set;}
    public String? Title {get; set;}
    public String? Type {get; set;}
    public DateTime CreatedAt {get; set;}
    [JsonIgnore]
    public ICollection<Developer> Developers { get; set;}
    }
}