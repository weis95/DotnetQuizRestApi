using Microsoft.EntityFrameworkCore;

namespace QuizApi.Models
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {
        }

        public DbSet<QuizItem> QuizItem { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
    }
}