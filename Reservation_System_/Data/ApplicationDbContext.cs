using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Reservation_System_.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<System_rezerwacji_sal.Models.Reservation> Reservation { get; set; }
        public DbSet<System_rezerwacji_sal.Models.Room> Room { get; set; }
        public DbSet<Reservation_System_.Models.ViewModels.LoginViewModel> LoginViewModel { get; set; }
        public DbSet<Reservation_System_.Models.User> User { get; set; }
    }
}