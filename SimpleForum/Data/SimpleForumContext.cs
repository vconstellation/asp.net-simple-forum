using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleForum.Models;

namespace SimpleForum.Data
{
    public class SimpleForumContext : IdentityDbContext
    {
        public SimpleForumContext(DbContextOptions<SimpleForumContext> options) : base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<ForumThread>? Threads { get; set; }
        public DbSet<Post>? Posts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            var connectionString = configuration.GetConnectionString("Sqlite");

            options.UseSqlite(connectionString);
        }
    }
}
