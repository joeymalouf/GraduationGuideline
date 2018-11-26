using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GraduationGuideline.data.entities;
using Microsoft.EntityFrameworkCore;

namespace GraduationGuideline.data
{
   public class GraduationGuidelineContext : DbContext
    {
        // For migrations
        public GraduationGuidelineContext() 
            : this(new DbContextOptionsBuilder<GraduationGuidelineContext>().UseNpgsql("User Id=GraduationGuideline;Password=jubjub67;Host=localhost;Port=5432;Database=GraduationGuideline").Options) { }

        // Inject in Startup
        public GraduationGuidelineContext(DbContextOptions<GraduationGuidelineContext> options) : base(options)
        { 
            
        }

        
        public DbSet<UserEntity> User { get; set; }
        public DbSet<StepEntity> Step { get; set; }
        public DbSet<DeadlineEntity> Deadline { get; set; }

        public string ProviderName => base.Database.ProviderName;

        public void Migrate()
        {
            this.Database.Migrate();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().HasKey(m => m.Username);

            builder.Entity<StepEntity>()
                .HasKey(u => new {u.Username, u.StepName});

            builder.Entity<DeadlineEntity>().HasKey(m => new {m.Semester, m.year} );

            base.OnModelCreating(builder);
        }

    }
}