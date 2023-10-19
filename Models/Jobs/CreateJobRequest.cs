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
        public required String Url {get; set;}
        [Required]
        public required String JobTechId {get; set;}
        [Required]
        public required String JobText {get; set;}
        public IEnumerable<Guid>? SelectedSkillIds {get; set;}
    }
}