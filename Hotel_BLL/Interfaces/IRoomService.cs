using Hotel_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetAllRooms();
        RoomDTO Get(int id);
        int AddRoom(RoomDTO roomDTO);
        void DeleteRoom(int id);
        void UpdateRoom(RoomDTO roomDTO);
    }
}
