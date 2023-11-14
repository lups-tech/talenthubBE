using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models
{
    public class ProposedDataDTO
    {
        public Guid Id {get; set;}
        public DateTime? Date {get; set;}
        public bool Succeeded {get; set;}
    }
}