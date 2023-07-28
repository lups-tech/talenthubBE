using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Developers
{
    public class CreateDeveloperSkillsRequest
    {
        public Guid developerId {get; set;}
        public IEnumerable<Guid> selectedSkillIds {get; set;} = default!;
    }

    public class DeleteDeveloperSkillsRequest
    {
        public Guid developerId {get; set;}
        public Guid SkillId {get; set;}
    }
}