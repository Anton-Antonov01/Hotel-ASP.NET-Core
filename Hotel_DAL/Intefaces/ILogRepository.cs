using Hotel_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAL.Intefaces
{
    public interface ILogRepository
    {

        public Log Get(int id);
        public IEnumerable<Log> GetAll();
        public void CreateLog(Log log);
    }
}
