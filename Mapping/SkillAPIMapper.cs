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

        public static Skill ToSkill (this CreateSkillRequest newSkill)
        {
            return new Skill
            {
                Id = new Guid(),
                Title = newSkill.Title,
                Type = newSkill.Type,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };
        }
    }
}