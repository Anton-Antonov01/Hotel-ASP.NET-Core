using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.DTO
{
    public class LogDataDTO
    {
        public int AdminId { get; set; }
        public string EntityName { get; set; }
        public int? EntityId { get; set; }
        public string ObjectState { get; set; }
    }
}
