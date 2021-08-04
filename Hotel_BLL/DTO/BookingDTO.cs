using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public bool Set { get; set; }
        public UserDTO user { get; set; }
        public RoomDTO room { get; set; }

        public override string ToString()
        {
            return $"BookingDate: {BookingDate}, EnterDate: {EnterDate}, LeaveDate: {LeaveDate}, Set: {Set}, RoomId: {room.Id}, UserId: {user.Id}";
        }
    }
}
