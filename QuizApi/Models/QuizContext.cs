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
    }
}