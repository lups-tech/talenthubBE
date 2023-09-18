using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Skills
{
    public class SkillScraperRequest
    {
        [Required]
        public required string Description { get; set; }
    }
}