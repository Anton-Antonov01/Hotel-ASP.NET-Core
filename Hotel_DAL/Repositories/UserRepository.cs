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
    class UserRepository : IRepository<User>
    {
        HotelContext db;

        public UserRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public int Create(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();

            return user.Id;
        }

        public void Delete(int id)
        {
            User user = Get(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public void Update(User user)
        {
            var toUpdate = db.Users.FirstOrDefault(m => m.Id == user.Id);
            if (toUpdate != null)
            {
                toUpdate.Id = user.Id;
                toUpdate.Name = user.Name ?? toUpdate.Name;
                toUpdate.Surname = user.Surname ?? toUpdate.Surname;
                toUpdate.PhoneNumber = user.PhoneNumber ?? toUpdate.PhoneNumber;
                toUpdate.Address = user.Address ?? toUpdate.Address;
                toUpdate.UserName = user.PhoneNumber ?? toUpdate.PhoneNumber;
                toUpdate.NormalizedUserName = toUpdate.UserName;
            }

        }

    }
}
