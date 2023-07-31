using Microsoft.EntityFrameworkCore;
using talenthubBE.Models.Developers;

public class MvcDataContext : DbContext
    {
        public MvcDataContext (DbContextOptions<MvcDataContext> options)
            : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; } = default!;

        public DbSet<Job> JobDescriptions { get; set; } = default!;

        public DbSet<Skill> Skills { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>()
                .HasMany(ep => ep.Skills)
                .WithMany(e => e.Developers)
                .UsingEntity(j => j.ToTable("DeveloperSkill"));
        }
    }
