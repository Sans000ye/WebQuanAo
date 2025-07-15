using Microsoft.EntityFrameworkCore;
using ClothingWebsite.Server.Models.Domains;


namespace ClothingWebsite.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }
    }
}
