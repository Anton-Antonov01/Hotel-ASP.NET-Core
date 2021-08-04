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
    class LogRepository : ILogRepository
    {
        HotelContext db;

        public LogRepository(HotelContext db)
        {
            this.db = db;
        }


        public Log Get(int id)
        {
            return db.Logs.Find(id);
        }

        public IEnumerable<Log> GetAll()
        {
            return db.Logs;
        }

        public void CreateLog(Log log)
        {
            db.Add(log);
        }
    }
}
