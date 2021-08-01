using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.RequestModels
{
    public class RoomBookingRequest 
    {
        [Display(Name = "Дата бронирования")]
        [Required]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Дата въезда")]
        [Required]
        public DateTime EnterDate { get; set; }

        [Display(Name = "Колличество дней")]
        [Required(ErrorMessage = "Это обязательное значение")]
        public int NumberOfDays { get; set; }



    }
}
