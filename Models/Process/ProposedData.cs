using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models
{
    public class ProposedData
    {
        public Guid Id {get; set;}
        public DateTime Date {get; set;}
        public bool Succeeded {get; set;}
        public Guid MatchingProcessId {get; set;}
        public MatchingProcess MatchingProcess {get; set;} = null!;
    }
}