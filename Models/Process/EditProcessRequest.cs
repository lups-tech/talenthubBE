using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models
{
    public class EditProcessRequest
    {
        public Guid Id {get; set;}
        public bool? Placed { get; set; }
        public DateTime? ResultDate {get; set;}
    }
}