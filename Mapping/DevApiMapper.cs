using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models.Developers;

namespace talenthubBE.Mapping
{
    public static class DevApiMapper
    {
        public static DeveloperDTO ToDevDTO (this Developer developer)
        {
            return new DeveloperDTO
            {
                Id = developer.Id,
                Name = developer.Name,
                Email = developer.Email,
                Skills = developer.Skills.SkillsMapper(),
            };
        }
        public static DevSkillDTO ToDevSkillDTO(this Skill skill)
        {
            return new DevSkillDTO
            {
                Id = skill.Id,
                Title = skill.Title,
                Type = skill.Type,
            };
        }

        public static List<DeveloperSkill> ToDeveloperSkill(this CreateDeveloperSkillsRequest request)
        {
            List<DeveloperSkill> developerSkillList = new();
            foreach (Guid entry in request.selectedSkillIds)
            {
                DeveloperSkill newSkill = new();
                newSkill.DevelopersId = request.developerId;
                newSkill.SkillsId = entry;
                developerSkillList.Add(newSkill);
            }
            return developerSkillList;
        }
        private static List<DevSkillDTO> SkillsMapper (this ICollection<Skill> skills)
        {
            List<DevSkillDTO> output = new();
            foreach (Skill skill in skills)
            {
                output.Add(skill.ToDevSkillDTO());
            }

            return output;
        }
    }
}