using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<City> City { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Setting> Settings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<City>()
                .HasMany(c => c.Hotels)
                .WithOne(h => h.City)
                .HasForeignKey(h => h.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Hotel>()
                .HasMany(h => h.Comments)
                .WithOne(c => c.Hotel)
                .HasForeignKey(c => c.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Hotel>()
                .HasMany(h => h.HotelImages)
                .WithOne(i => i.Hotel)
                .HasForeignKey(i => i.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Hotel>()
                .HasMany(h => h.WishLists)
                .WithOne(w => w.Hotel)
                .HasForeignKey(w => w.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Room>()
                .HasMany(r => r.RoomImages)
                .WithOne(i => i.Room)
                .HasForeignKey(i => i.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Room>()
                .HasMany(r => r.Reservations)
                .WithOne(res => res.Room)
                .HasForeignKey(res => res.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Room>()
                .Property(r => r.Price)
                .HasPrecision(18, 2);

            builder.Entity<Room>()
                .Property(r => r.Area)
                .HasPrecision(10, 2); 


            builder.Entity<Reservation>()
                .HasOne(res => res.AppUser)
                .WithMany(u => u.Reservations)
                .HasForeignKey(res => res.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

      
            builder.Entity<WishList>()
                .HasKey(w => new { w.AppUserId, w.HotelId });

            builder.Entity<WishList>()
                .HasOne(w => w.AppUser)
                .WithMany(u => u.WishLists)
                .HasForeignKey(w => w.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WishList>()
                .HasOne(w => w.Hotel)
                .WithMany(h => h.WishLists)
                .HasForeignKey(w => w.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Blog>()
                .HasOne(b => b.BlogCategory)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
