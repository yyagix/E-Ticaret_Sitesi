using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductApp.Models;

namespace ProductApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Products>? Products { get; set; }
        public DbSet<Slider>? Sliders { get; set; }
        public DbSet<AppUser>? AppUsers { get; set; }
    }
}
