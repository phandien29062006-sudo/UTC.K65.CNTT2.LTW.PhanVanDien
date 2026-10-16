
using Microsoft.EntityFrameworkCore;
using NetCoreLA7.Areas.Admins.Models;

namespace NetCoreLA7.Areas.Admins.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Blog> Blogs { get; set; }

    }
}
