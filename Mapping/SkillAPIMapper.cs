using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models.Skills;

namespace talenthubBE.Mapping
{
    public static class SkillAPIMapper
    {
        public static SkillDTO ToSkillDTO (this Skill skill)
        {
            return new SkillDTO
            {
                Id = skill.Id,
                Title = skill.Title,
                Type = skill.Type
            };
        }
    }
}