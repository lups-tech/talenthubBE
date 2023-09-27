using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models.Developers;

namespace talenthubBE.Data
{
    public interface IDevelopersRepository
    {
        Task<IEnumerable<DeveloperDTO>?> GetAllDevelopers();
        Task<DeveloperDTO?> GetDeveloper(Guid id);
        Task<DeveloperDTO?> PutDeveloper(Guid id, Developer developer);
        Task<DeveloperDTO?> PostDeveloper(String authId, CreateDeveloperRequest request);
        Task DeleteDeveloper(Guid id);
        Task<DeveloperDTO> AddDeveloperSkills(CreateDeveloperSkillsRequest request);
        Task<bool> DeleteDeveloperSkill(DeleteDeveloperSkillsRequest request);
    }
}