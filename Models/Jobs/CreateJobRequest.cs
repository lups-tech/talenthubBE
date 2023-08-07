using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public IEnumerable<Guid> SelectedSkillIds {get; set;} = default!;
    }
}