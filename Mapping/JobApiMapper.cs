using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models.Jobs;

namespace talenthubBE.Mapping
{
    public static class JobApiMapper
    {
         public static JobDTO ToJobDTO (this Job job)
        {
            return new JobDTO
            {
                Id = job.Id,
                Url = job.Url,
                JobText = job.JobText,
                Skills = job.Skills.SkillsMapper(),
            };
        }
        public static JobSkillDTO ToJobSkillDTO(this Skill skill)
        {
            return new JobSkillDTO
            {
                Id = skill.Id,
                Title = skill.Title,
                Type = skill.Type,
            };
        }
        public static Job ToJob(this CreateJobRequest newJob)
        {
            return new Job
            {
                Id = new Guid(),
                Url = newJob.Url,
                JobText = newJob.JobText,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };
        }
        private static List<JobSkillDTO> SkillsMapper (this ICollection<Skill> skills)
        {
            List<JobSkillDTO> output = new();
            foreach (Skill skill in skills)
            {
                output.Add(skill.ToJobSkillDTO());
            }

            return output;
        }
    }
}