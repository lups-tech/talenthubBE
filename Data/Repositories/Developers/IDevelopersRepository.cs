using talenthubBE.Models.Developers;

namespace talenthubBE.Data
{
    public interface IDevelopersRepository
    {
        // task = async thing. IEnumerable = list of things. DeveloperDTO = Developer Data Transfer Object (like a schema for Developer). ? = nullable. 
        Task<IEnumerable<DeveloperDTO>?> GetAllDevelopers();
        Task<DeveloperDTO?> GetDeveloper(Guid id);
        Task<DeveloperDTO?> PutDeveloper(Guid id, Developer developer);
        Task<DeveloperDTO?> PostDeveloper(String authId, CreateDeveloperRequest request);
        Task DeleteDeveloper(Guid id);
        Task<DeveloperDTO> AddDeveloperSkills(CreateDeveloperSkillsRequest request);
        Task<bool> DeleteDeveloperSkill(DeleteDeveloperSkillsRequest request);
    }
}