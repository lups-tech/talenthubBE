using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models.Comments;

namespace talenthubBE.Models.Developers
{
    public class DeveloperDTO
    {
    public Guid Id {get; set;}
    public String? Name {get; set;}
    public String? Email {get; set;}
    public List<DevSkillDTO>? Skills {get; set;}
    public List<CommentDTO>? Comments {get; set;}
    public int SkillMatch {get; set;} = 0;
    }
}