using GrammarWorkbook.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GrammarWorkbook.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Unit> Units { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exercise>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<FillTheBlanksExercise>("fill")
                .HasValue<MatchTheWordsExercise>("match")
                .HasValue<DialogExercise>("dialogue");
        }
    }
}