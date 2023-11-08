using Microsoft.EntityFrameworkCore;
using talenthubBE.Models;
using talenthubBE.Models.Comments;

public class MvcDataContext : DbContext
    {
        public MvcDataContext (DbContextOptions<MvcDataContext> options)
            : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; } = default!;

        public DbSet<Job> JobDescriptions { get; set; } = default!;

        public DbSet<Skill> Skills { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;
        
        public DbSet<Organization> Organizations { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<MatchingProcess> MatchingProcesses { get; set; } = default!;
        public DbSet<InterviewData> Interviews { get; set; } = default!;
        public DbSet<ContractData> Contracts { get; set; } = default!;
        public DbSet<ProposedData> Proposals { get; set; } = default!;
        
    }
