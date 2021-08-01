using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.RequestModels
{
    public class BookingRequest
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата бронирования")]
        public DateTime BookingDate { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата въезда")]
        public DateTime EnterDate { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime LeaveDate { get; set; }



        [Required]
        [Display(Name = "Въехал ли гость")]//Скорее всего уберу
        public bool Set { get; set; }


        [Required]
        [Display(Name = "Id гостя")]
        public int GuestId { get; set; }
        [Required]
        [Display(Name = "Id комнаты")]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Id пользователя")]
        public int UserId { get; set; }

    }

}
