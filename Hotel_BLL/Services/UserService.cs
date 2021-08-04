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

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = Database.Users.GetAll();

            return Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public UserDTO Get(int id)
        {
            var user = Database.Users.Get(id);
            if (user == null)
                throw new NullReferenceException();

            return Mapper.Map<User, UserDTO>(user);
        }

        public UserDTO GetByPhoneNumber(string phone)
        {
            var user = Database.Users.GetAll().SingleOrDefault(u=> u.PhoneNumber == phone);

            return Mapper.Map<User, UserDTO>(user);
        }


        public void DeleteUser(int id)
        {
            if (Database.Users.Get(id) == null)
                throw new NullReferenceException();

            Database.Users.Delete(id);
            Database.Save();
        }

        public void UpdateUser(UserDTO userDTO)
        {
            var user = Database.Users.Get(userDTO.Id);
            if (user == null)
                throw new NullReferenceException();
            if (Database.Users.GetAll().Any(g => g.PhoneNumber == userDTO.PhoneNumber && g.Id != userDTO.Id))
                throw new ArgumentException();

            Database.Users.Update(Mapper.Map<UserDTO, User>(userDTO));
            Database.Save();
        }
    }
}
