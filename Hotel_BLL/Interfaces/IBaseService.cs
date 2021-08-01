using Hotel_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.Interfaces
{
    public interface IBaseService
    {
        ProfitByMonthDTO GetProfitByOneMonth(DateTime month);

        IEnumerable<ProfitByMonthDTO> GetProfitByMonths();

        IEnumerable<RoomDTO> FreeRoomsByDate(DateTime date);

        IEnumerable<RoomDTO> FreeRoomsByDateRange(DateTime firstDate, DateTime secondDate);

        void DoBooking(BookingDTO bookingDTO);
    }
}
