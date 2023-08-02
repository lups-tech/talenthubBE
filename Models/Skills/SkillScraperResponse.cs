using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models.Developers;

namespace talenthubBE.Models.Skills
{
    public class SkillScraperResponse
    {
        public ICollection<SkillDTO>? JobSkills {get; set;}
        public ICollection<DeveloperDTO>? Developers {get; set;}
    }
}