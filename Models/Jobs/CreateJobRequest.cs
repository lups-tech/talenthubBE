using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Jobs
{
    public class CreateJobRequest
    {
        public String? Url {get; set;}
        public String? JobText {get; set;}
        public IEnumerable<Guid> selectedSkillIds {get; set;} = default!;
    }
}