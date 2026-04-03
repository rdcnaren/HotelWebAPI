using FirstWeb_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb_API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Models.Hotel> Hotel { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.HotelAmenities> HotelAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>().HasData(
new Hotel { Id = 1, Name = "Ocean Breeze Resort", Details = "Luxury beach resort with ocean view.", Rate = 1000.00, ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945", Created = new DateTime(2024, 1, 1), Updated = new DateTime(2024, 1, 1) },
new Hotel { Id = 2, Name = "Mountain Paradise Lodge", Details = "Peaceful stay near mountains.", Rate = 964.00, ImageUrl = "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa", Created = new DateTime(2024, 1, 2), Updated = new DateTime(2024, 1, 2) },
new Hotel { Id = 3, Name = "City Grand Hotel", Details = "Modern hotel in city center.", Rate = 754.00, ImageUrl = "https://images.unsplash.com/photo-1578683010236-d716f9a3f461", Created = new DateTime(2024, 1, 3), Updated = new DateTime(2024, 1, 3) },
new Hotel { Id = 4, Name = "Royal Palace Stay", Details = "Luxury palace style hotel.", Rate = 537.00, ImageUrl = "https://images.unsplash.com/photo-1564501049412-61c2a3083791", Created = new DateTime(2024, 1, 4), Updated = new DateTime(2024, 1, 4) },
new Hotel { Id = 5, Name = "Sunset Cliff Resort", Details = "Beautiful sunset cliff view rooms.", Rate = 637.00, ImageUrl = "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb", Created = new DateTime(2024, 1, 5), Updated = new DateTime(2024, 1, 5) }
);
        }
    }
}
