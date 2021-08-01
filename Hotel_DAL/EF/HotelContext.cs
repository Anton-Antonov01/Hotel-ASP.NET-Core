using Hotel_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAL.EF
{
    public class HotelContext : IdentityDbContext<User, IdentityRole<int> ,int >
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {

        }

        internal DbSet<Category> Categories { get; set; }
        internal DbSet<Room> Rooms { get; set; }
        internal DbSet<User> Users { get; set; }
        internal DbSet<Booking> Bookings { get; set; }
        internal DbSet<PriceCategory> PriceCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                //new User { Id=1, Name="Tom", Age=23},

                });
        }

    }
}
