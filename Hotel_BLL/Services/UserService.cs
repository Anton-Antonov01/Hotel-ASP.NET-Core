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
    public class UserService : IUserService
    {
        IWorkUnit Database { get; set; }
        IMapper Mapper;

        public UserService(IWorkUnit database)
        {
            Database = database;
            Mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<User, UserDTO>().ReverseMap()
                ).CreateMapper();
        }

        public IEnumerable<UserDTO> GetAllGuests()
        {
            var guests = Database.Users.GetAll();

            return Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(guests);
        }

        public UserDTO Get(int id)
        {
            var user = Database.Users.Get(id);
            if (user == null)
                throw new ArgumentException();

            return Mapper.Map<User, UserDTO>(user);
        }

        public UserDTO GetByPhoneNumber(string phone)
        {
            var user = Database.Users.GetAll().SingleOrDefault(u=> u.PhoneNumber == phone);

            return Mapper.Map<User, UserDTO>(user);
        }

        public void AddGuest(UserDTO guestDTO)
        {
            if (Database.Users.GetAll().Any(g => g.PhoneNumber == guestDTO.PhoneNumber))
                throw new ArgumentException();

            Database.Users.Create(Mapper.Map<UserDTO, User>(guestDTO));
            Database.Save();
        }

        public void DeleteGuest(int id)
        {
            if (Database.Users.Get(id) == null)
                throw new ArgumentException();

            Database.Users.Delete(id);
            Database.Save();
        }

        public void UpdateGuest(UserDTO guestDTO)
        {
            var guest = Database.Users.Get(guestDTO.Id);
            if (guest == null)
                throw new NullReferenceException();
            if (Database.Users.GetAll().Any(g => g.PhoneNumber == guestDTO.PhoneNumber && g.Id != guestDTO.Id))
                throw new ArgumentException();

            Database.Users.Update(Mapper.Map<UserDTO, User>(guestDTO));
            Database.Save();
        }
    }
}
