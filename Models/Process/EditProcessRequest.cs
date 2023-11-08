using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models
{
    public class EditProcessRequest
    {
        public Guid Id {get; set;}
        public ProposedDataDTO? Proposed {get; set;}
        public IEnumerable<InterviewDataDTO> Interviews {get; set;}
        public IEnumerable<ContractDataDTO> Contracts {get; set;}
        public bool? Placed { get; set; }
        public DateTime? ResultDate {get; set;}
    }
}