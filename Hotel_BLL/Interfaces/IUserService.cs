using Hotel_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllGuests();
        UserDTO Get(int id);

        UserDTO GetByPhoneNumber(string phone);

        void AddGuest(UserDTO guestDTO);
        void DeleteGuest(int id);
        void UpdateGuest(UserDTO guestDTO);
    }
}
