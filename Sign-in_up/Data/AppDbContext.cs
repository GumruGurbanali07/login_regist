using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Sign_in_up.Models;

namespace Sign_in_up.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users {  get; set; }
    }
}
