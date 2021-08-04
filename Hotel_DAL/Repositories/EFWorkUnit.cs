using Hotel_DAL.EF;
using Hotel_DAL.Entities;
using Hotel_DAL.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAL.Repositories
{
    public class EFWorkUnit : IWorkUnit
    {
        private HotelContext db;
        private RoomRepository roomRepository;
        private CategoryRepository categoryRepository;
        private UserRepository userRepository;
        private BookingRepository bookingRepository;
        private PriceCategoryRepository priceCategoryRepository;
        private LogRepository logRepository;


        public EFWorkUnit(HotelContext context)
        {
            db = context; 
        }

        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepository(db);
                }
                return roomRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }
                return categoryRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                {
                    bookingRepository = new BookingRepository(db);
                }
                return bookingRepository;
            }
        }

        public IRepository<PriceCategory> PriceCategories
        {
            get
            {
                if (priceCategoryRepository == null)
                {
                    priceCategoryRepository = new PriceCategoryRepository(db);
                }
                return priceCategoryRepository;
            }
        }

        public ILogRepository Logs
        {
            get
            {
                if (logRepository == null)
                {
                    logRepository = new LogRepository(db);
                }
                return logRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
