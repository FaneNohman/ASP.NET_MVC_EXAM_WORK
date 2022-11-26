using Microsoft.EntityFrameworkCore;

namespace CORE_MVC_EXAM.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<UserLesson> UserLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserLesson>().HasKey(sc => new { sc.UserId, sc.LessonId });

            modelBuilder.Entity<UserLesson>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserLessons)
                .HasForeignKey(sc => sc.UserId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLesson>()
                .HasOne<Lesson>(sc => sc.Lesson)
                .WithMany(s => s.UserLessons)
                .HasForeignKey(sc => sc.LessonId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
