using talenthubBE.Models.Skills;

namespace talenthubBE.Data
{
    public interface ISkillsRepository
    {
        Task<IEnumerable<SkillDTO>?> GetAllSkills();
        Task<SkillDTO?> GetSkill(Guid id);
        Task<SkillDTO?> PutSkill(Guid id, Skill skill);
        Task<SkillDTO?> PostSkill(CreateSkillRequest request);
        Task DeleteSkill(Guid id);
        Task<IEnumerable<SkillDTO>> ScrapeSkills(SkillScraperRequest text);
        Task<SkillScraperResponse> SkillMatchDevs(IEnumerable<SkillDTO> jobSkills, string orgId);
    }
}