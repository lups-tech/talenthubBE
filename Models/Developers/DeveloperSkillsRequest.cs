using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Developers
{
    public class CreateDeveloperSkillsRequest
    {
        public Guid DeveloperId {get; set;}
        public IEnumerable<Guid> SelectedSkillIds {get; set;} = default!;
    }

    public class DeleteDeveloperSkillsRequest
    {
        [Required]
        public Guid DeveloperId {get; set;}
        [Required]
        public Guid SkillId {get; set;}
    }
}