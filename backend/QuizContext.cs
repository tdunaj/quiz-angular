using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<backend.Models.Quiz> Quiz { get; set; }

    }
}
