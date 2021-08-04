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
            Database.EnsureCreated();
        }

        internal DbSet<Category> Categories { get; set; }
        internal DbSet<Room> Rooms { get; set; }
        internal DbSet<User> Users { get; set; }
        internal DbSet<Booking> Bookings { get; set; }
        internal DbSet<PriceCategory> PriceCategories { get; set; }
        internal DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category[]
                {
                    new Category()
                    {
                        Id=1,
                        Name = "Стандарт",
                        Bed = 1,
                        Description = "Описание категории стандарт с одной кроватью"

                    },
                    new Category()
                    {
                        Id=2,
                        Name = "Стандарт",
                        Bed = 2,
                        Description = "Описание категории стандарт с двумя кроватями"

                    },
                    new Category()
                    {
                        Id=3,
                        Name = "Люкс",
                        Bed = 1,
                        Description = "Описание категории люкс с одной кроватью"

                    },
                    new Category()
                    {
                        Id=4,
                        Name = "Люкс",
                        Bed = 2,
                        Description = "Описание категории люкс с двумя кроватями"
                    },
                });

                modelBuilder.Entity<Room>().HasData(
                new Room[]
                {
                    new Room()
                    {
                        Id=1,
                        Name = "101",
                        CategoryId = 1,
                        Active = true
                    },
                 
                    new Room()
                    {
                        Id=2,
                        Name = "102",
                        CategoryId = 2,
                        Active = true
                    },
                 
                    new Room()
                    {
                        Id=3,
                        Name = "201",
                        CategoryId = 3,
                        Active = true                  
                    },
                 
                    new Room()
                    {
                        Id=4,
                        Name = "202",
                        CategoryId = 4,
                        Active = true
                    }
                });

                

                //modelBuilder.Entity<Booking>().HasData(
                //new Booking[]
                //{
                //    new Booking()
                //    {
                //        Id = 1,
                //        UserId = 2,
                //        RoomId = 1,
                //        Set = false,
                //        BookingDate = new DateTime(2021, 3, 5),
                //        EnterDate = new DateTime(2021, 3, 6),
                //        LeaveDate = new DateTime(2021, 3, 10),
                //    },
     
                //    new Booking()
                //    {
                //        Id = 1,
                //        UserId = 2,
                //        RoomId = 1,
                //        Set = true,
                //        BookingDate = new DateTime(2021, 3, 15),
                //        EnterDate = new DateTime(2021, 3, 16),
                //        LeaveDate = new DateTime(2021, 3, 20),
                //    },
     
                //    new Booking()
                //    {
                //        Id = 1,
                //        UserId = 2,
                //        RoomId = 2,
                //        Set = true,
                //        BookingDate = new DateTime(2021, 4, 2),
                //        EnterDate = new DateTime(2021, 4, 10),
                //        LeaveDate = new DateTime(2021, 4, 20),
                //    },
     
                //    new Booking()
                //    {
                //        Id = 1,
                //        UserId = 2,
                //        RoomId = 3,
                //        Set = true,
                //        BookingDate = new DateTime(2021, 4, 15),
                //        EnterDate = new DateTime(2021, 4, 16),
                //        LeaveDate = new DateTime(2021, 5, 20),
                //    },
                //});

                modelBuilder.Entity<PriceCategory>().HasData(
                new PriceCategory[]
                {
                    new PriceCategory()
                    {
                        Id = 1,
                        CategoryId = 1,
                        Price = 100,
                        StartDate = new DateTime(2017, 3, 15),
                        EndDate = new DateTime(2022, 3, 15),
                    },
         
                    new PriceCategory()
                    {
                        Id = 2,
                        CategoryId = 2,
                        Price = 200,
                        StartDate = new DateTime(2017, 3, 15),
                        EndDate = new DateTime(2022, 3, 15),
                    },
         
                    new PriceCategory()
                    {
                        Id = 3,
                        CategoryId = 3,
                        Price = 300,
                        StartDate = new DateTime(2017, 3, 15),
                        EndDate = new DateTime(2021, 4, 20),
                    },
         
                    new PriceCategory()
                    {
                        Id = 4,
                        CategoryId = 3,
                        Price = 1000,
                        StartDate = new DateTime(2021, 4, 20),
                        EndDate = new DateTime(2022, 3, 15),
                    },
         
                    new PriceCategory()
                    {
                        Id = 5,
                        CategoryId = 4,
                        Price = 1400,
                        StartDate = new DateTime(2017, 3, 15),
                        EndDate = new DateTime(2022, 3, 15),
                    },
                });            
        }

    }
}
