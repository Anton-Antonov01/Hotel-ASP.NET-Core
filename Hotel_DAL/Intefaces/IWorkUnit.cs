using Hotel_DAL.Entities;
using Hotel_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAL.Intefaces
{
    public interface IWorkUnit
    {
        IRepository<Room> Rooms { get; }
        IRepository<Category> Categories { get; }
        IRepository<User> Users { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<PriceCategory> PriceCategories { get; }
        ILogRepository Logs { get; }
        void Save();
    }
}
