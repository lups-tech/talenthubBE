using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Jobs
{
    public class CreateJobRequest
    {
        [Required]
        public String? Url {get; set;}
        [Required]
        public String? JobText {get; set;}
        public IEnumerable<Guid> selectedSkillIds {get; set;} = default!;
    }
}