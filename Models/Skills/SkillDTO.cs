using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Skills
{
    public class SkillDTO
    {
        public Guid Id {get; set;}
        public String? Title {get; set;}
        public String? Type {get; set;}
    }
}