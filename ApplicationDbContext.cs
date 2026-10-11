using Microsoft.EntityFrameworkCore;
using LocalServiceBooking.Models;

namespace LocalServiceBooking.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ServiceProvider> ServiceProviders { get; set; }
}
