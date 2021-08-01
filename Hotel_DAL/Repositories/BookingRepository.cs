using Hotel_DAL.EF;
using Hotel_DAL.Entities;
using Hotel_DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAL.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        HotelContext db;

        public BookingRepository(HotelContext db)
        {
            this.db = db;
        }

        public Booking Get(int id)
        {
            return db.Bookings.Include(u => u.user).Include(u => u.room).ThenInclude(r => r.RoomCategory).Single(b => b.Id == id);
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings.Include(u => u.user).Include(u => u.room).ThenInclude(r => r.RoomCategory).ToList();
        }

        public void Create(Booking booking)
        {

            booking.user = db.Users.Find(booking.user.Id); 
            booking.room = db.Rooms.Find(booking.room.Id);

            db.Bookings.Add(booking);
        }

        public void Delete(int id)
        {
            db.Bookings.Remove(Get(id));
        }

        public void Update(Booking booking)
        {
            var toUpdate = db.Bookings.FirstOrDefault(m => m.Id == booking.Id);
            if (toUpdate != null)
            {

                toUpdate.user = db.Users.Find(booking.user.Id) ?? toUpdate.user; 
                toUpdate.room = db.Rooms.Find(booking.room.Id) ?? toUpdate.room;

                toUpdate.Id = booking.Id;
                toUpdate.BookingDate = booking.BookingDate;
                toUpdate.EnterDate = booking.EnterDate;
                toUpdate.LeaveDate = booking.LeaveDate;
                toUpdate.Set = booking.Set;
                toUpdate.RoomId = booking.RoomId;
                toUpdate.UserId = booking.UserId;
            }
        }
    }
}
