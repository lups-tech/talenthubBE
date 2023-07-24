using Microsoft.EntityFrameworkCore;

public class MvcDeveloperContext : DbContext
    {
        public MvcDeveloperContext (DbContextOptions<MvcDeveloperContext> options)
            : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; } = default!;
    }
