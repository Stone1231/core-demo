using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace coreDemo.Entity
{
    public class TestContext : DbContext
    {
        public DbSet<ClassM> ClassMs { get; set; }

        public DbSet<ClubM> ClubMs { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Filename=./test.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Á×§K may cause cycles or multiple cascade paths
            //foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            builder.Entity<StudClub>()
                .HasKey(t => new { t.StudentId, t.ClubId });

            builder.Entity<StudClub>()
                .HasOne(pt => pt.Student)
                .WithMany(p => p.StudClubs)
                .HasForeignKey(pt => pt.StudentId);

            builder.Entity<StudClub>()
                .HasOne(pt => pt.Club)
                .WithMany(t => t.StudClubs)
                .HasForeignKey(pt => pt.ClubId);

            base.OnModelCreating(builder);
        }

            public TestContext(DbContextOptions<TestContext> options)
      : base(options)
        { }
    }
}