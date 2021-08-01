using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.RequestModels
{
    public class UserRequest
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
    }
}
