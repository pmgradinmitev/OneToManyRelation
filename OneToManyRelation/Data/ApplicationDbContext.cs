using Microsoft.EntityFrameworkCore;
using OneToManyRelation.Data.Entities;

namespace OneToManyRelation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasOne(s => s.Teacher)             // each student has one teacher
            .WithMany(t => t.Students)          // each teacher has many students
            .HasForeignKey(s => s.TeacherId)    // use TeacherId as FK
            .IsRequired(false)                  // makes the relationship optional
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
