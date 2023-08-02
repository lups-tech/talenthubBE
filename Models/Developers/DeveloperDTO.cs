using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Developers
{
    public class DeveloperDTO
    {
    public Guid Id {get; set;}
    public String? Name {get; set;}
    public String? Email {get; set;}
    public List<DevSkillDTO>? Skills {get; set;}
    public int skillMatch {get; set;} = 0;
    }
}