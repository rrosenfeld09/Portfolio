using Microsoft.EntityFrameworkCore;

namespace ManifestSoftware.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) {}

        public DbSet<User> users {get; set;}

        public DbSet<Load> loads {get; set;}

        public DbSet<Manifest> manifests {get; set;}

        public DbSet<Post> posts {get; set;}

        public DbSet<Comment> comments {get; set;}
    }
}