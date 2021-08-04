using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.DTO
{
    public class LogDTO
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public DateTime Date { get; set; }
        public string EntityName { get; set; }
        public int? EntityId { get; set; }
        public string Operation { get; set; }
        public string ObjectState { get; set; }
    }
}
