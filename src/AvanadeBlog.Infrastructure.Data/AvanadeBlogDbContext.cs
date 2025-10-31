using AvanadeBlog.Domain.Entities;
using AvanadeBlog.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AvanadeBlog.Infrastructure.Data
{
    public class AvanadeBlogDbContext : DbContext
    {
        public AvanadeBlogDbContext(DbContextOptions<AvanadeBlogDbContext> options) : base(options) { }

        public DbSet<PostEntity> Blog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
        }
    }
}
