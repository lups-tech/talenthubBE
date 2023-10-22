using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Jobs
{
    public class JobDTO
    {
        public Guid Id {get; set;}
        public String? JobTechId {get; set;}
        public string? Title {get; set;}
        public string? Deadline {get; set;}
        public string? Employer {get; set;}
        public String? Url {get; set;}
        public String? JobText {get; set;}
        public List<JobSkillDTO>? Skills {get; set;}
    }
}