using GrammarWorkbook.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GrammarWorkbook.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<FillTheBlanksExercise> FillTheBlanksExercises { get; set; }
        public DbSet<Sentence> Sentences { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exercise>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<FillTheBlanksExercise>("fill")
                .HasValue<MatchTheWordsExercise>("match");
        }
    }
}