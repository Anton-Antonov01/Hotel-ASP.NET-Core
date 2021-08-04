using AutoMapper;
using Hotel_BLL.DTO;
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
    public class LogService : ILogService 
    {
        private IMapper mapper;
        private IWorkUnit Database { get; set; }

        public LogService(IWorkUnit database)
        {
            this.Database = database;

            mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Log, LogDTO>().ReverseMap()
                ).CreateMapper();

        }

        public IEnumerable<LogDTO> GetAll()
        {
            var Logs = mapper.Map<IEnumerable<Log>,IEnumerable<LogDTO>>(Database.Logs.GetAll());
            return Logs;
        }

        public LogDTO Get(int id)
        {
            return mapper.Map<Log,LogDTO>(Database.Logs.Get(id));
        }

        public void AddCreateLog(LogDataDTO logData)
        {
            var log = new Log()
            {
                AdminId = logData.AdminId,
                EntityName = logData.EntityName,
                Date = DateTime.Now,
                Operation = "Create",
                ObjectState = logData.ObjectState,
                EntityId = logData.EntityId
            };

            Database.Logs.CreateLog(log);
            Database.Save();
        }

        public void AddEditLog(LogDataDTO logData)
        {
            var log = new Log()
            {
                AdminId = logData.AdminId,
                EntityName = logData.EntityName,
                Date = DateTime.Now,
                Operation = "Edit",
                ObjectState = logData.ObjectState,
                EntityId = logData.EntityId,
            };

            Database.Logs.CreateLog(log);
            Database.Save();
        }

        public void AddDeleteLog(LogDataDTO logData)
        {
            var log = new Log()
            {
                AdminId = logData.AdminId,
                EntityName = logData.EntityName,
                Date = DateTime.Now,
                Operation = "Delete",
                ObjectState = logData.ObjectState,
                EntityId = logData.EntityId,
            };

            Database.Logs.CreateLog(log);
            Database.Save();
        }

    }
}
