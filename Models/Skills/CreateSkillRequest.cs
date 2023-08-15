using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Skills
{
    public class CreateSkillRequest
    {
        public required string Title {get; set;}
        public required string? Type {get; set;}
    }
}