using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Models
{
    public class UserModel
    {
        [Display(Name = "Id пользователя")]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }
    }
}
