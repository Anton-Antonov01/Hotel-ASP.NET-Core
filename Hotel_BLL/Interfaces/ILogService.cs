using Hotel_BLL.DTO;
using Hotel_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.Interfaces
{
    public interface ILogService
    {

        public IEnumerable<LogDTO> GetAll();


        public LogDTO Get(int id);


        public void AddCreateLog(LogDataDTO logData);

        public void AddEditLog(LogDataDTO logData);

        public void AddDeleteLog(LogDataDTO logData);

    }
}
