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
    class RoomRepository : IRepository<Room>
    {
        HotelContext db;

        public RoomRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms.Include(u => u.RoomCategory).ToList(); 
        }

        public Room Get(int id)
        {
            return db.Rooms.Include(u => u.RoomCategory).SingleOrDefault(r => r.Id == id);
        }

        public int Create(Room room)
        {
            room.RoomCategory = db.Categories.Find(room.RoomCategory.Id);
            db.Rooms.Add(room);
            db.SaveChanges();
            return room.Id;
        }

        public void Delete(int id)
        {
            Room room = Get(id);
            if (room != null)
                db.Rooms.Remove(room);
        }

        public void Update(Room room)
        {
            var toUpdate = db.Rooms.FirstOrDefault(m => m.Id == room.Id);
            if (toUpdate != null)
            {
                toUpdate.RoomCategory = db.Categories.Find(room.RoomCategory.Id) ?? toUpdate.RoomCategory;
                toUpdate.Id = room.Id;
                toUpdate.Name = room.Name ?? toUpdate.Name;
                toUpdate.Active = room.Active;
            }
        }
    }
}
