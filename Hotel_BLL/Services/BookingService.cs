using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.HelpClasses;
using Hotel_BLL.Interfaces;
using Hotel_DAL.Entities;
using Hotel_DAL.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.Services
{
    public class BookingService : IBookingService
    {
        IWorkUnit Database;
        IMapper Mapper;

        public BookingService(IWorkUnit database)
        {
            Database = database;

            Mapper = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<BookingDTO, Booking>().ReverseMap();
                    cfg.CreateMap<RoomDTO, Room>().ReverseMap();
                    cfg.CreateMap<UserDTO, User>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, Category>().ReverseMap();
                })
                .CreateMapper();
        }

        public BookingDTO Get(int id)
        {
            var booking = (Database.Bookings.Get(id));
            if (booking == null)
                throw new ArgumentException();

            return Mapper.Map<Booking, BookingDTO>(booking);
        }

        public IEnumerable<BookingDTO> GetAllBookings()
        {
            var bookings = Database.Bookings.GetAll();

            return Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(bookings);
        }

        public int AddBooking(BookingDTO bookingDTO)
        {
            if (Database.Rooms.Get(bookingDTO.room.Id) == null ||
                Database.Users.Get(bookingDTO.user.Id) == null)
                throw new ArgumentNullException();

            if (!FreeRoomsByDateRange(bookingDTO.BookingDate, bookingDTO.LeaveDate).Any(freeRoom => bookingDTO.room.Id == freeRoom.Id))
                throw new ArgumentException();
            
            
            return Database.Bookings.Create(Mapper.Map<BookingDTO, Booking>(bookingDTO));
            
        }

        public void DeleteBooking(int id)
        {
            if (Database.Bookings.Get(id) == null)
                throw new ArgumentException();

            Database.Bookings.Delete(id);
            Database.Save();
        }

        public void UpdateBooking(BookingDTO bookingDTO)
        {
            if (Database.Bookings.Get(bookingDTO.Id) == null ||
                Database.Rooms.Get(bookingDTO.room.Id) == null ||
                Database.Users.Get(bookingDTO.user.Id) == null)
                throw new NullReferenceException();

            if (!FreeRoomsByDateRange(bookingDTO.BookingDate, bookingDTO.LeaveDate, bookingDTO.Id).Any(freeRoom => bookingDTO.room.Id == freeRoom.Id))
                throw new ArgumentException();

            Database.Bookings.Update(Mapper.Map<BookingDTO, Booking>(bookingDTO));
            Database.Save();
        }


        private IEnumerable<RoomDTO> FreeRoomsByDateRange(DateTime firstDate, DateTime secondDate, int bookingId = 0)
        {
            List<RoomDTO> BookedRoomsForDate = new List<RoomDTO>();
            var bookings = Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(Database.Bookings.GetAll());
            var AllRooms = Mapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(Database.Rooms.GetAll());

            foreach (var booking in bookings)
            {
                if (booking.BookingDate <= firstDate && booking.LeaveDate > firstDate ||
                    booking.BookingDate <= firstDate && booking.LeaveDate > secondDate ||
                    booking.BookingDate >= firstDate && booking.LeaveDate < secondDate)
                {
                    BookedRoomsForDate.Add(AllRooms.FirstOrDefault(r => r.Id == booking.room.Id && booking.Id != bookingId));
                }
            }

            var NotBookedRooms = AllRooms.Except(BookedRoomsForDate);

            return NotBookedRooms;
        }

        public decimal GetTotalPrice(int id)
        {
            decimal TotalPrice = 0;

            var booking = Database.Bookings.Get(id);
            var priceCategories = Database.PriceCategories.GetAll().Where(pc => pc.CategoryId == booking.room.RoomCategory.Id).ToList();

            Interval BookingInterval = new Interval(booking.EnterDate, booking.LeaveDate);
            Interval PriceCategoryInterval = new Interval();

            foreach (var priceCategory in priceCategories)
            {
                PriceCategoryInterval.Start = priceCategory.StartDate;
                PriceCategoryInterval.End = priceCategory.EndDate;

                if (PriceCategoryInterval.IsInclude(BookingInterval))
                {
                    TotalPrice += priceCategory.Price * (PriceCategoryInterval.DaysIncludes(BookingInterval));
                }
            }

            return TotalPrice;
        }
    }
}
