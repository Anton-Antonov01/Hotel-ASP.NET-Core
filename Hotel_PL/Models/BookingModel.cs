using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Models
{
    public class BookingModel
    {
        [Display(Name = "№ брони")]
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Дата брони")]
        public DateTime BookingDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Дата въезда")]
        public DateTime EnterDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Дата выезда")]
        public DateTime LeaveDate { get; set; }

        [Display(Name = "Заселение")]
        public bool Set { get; set; }

        [Display(Name = "Стоимость")]
        public decimal TotalPrice { get; set; }
        public virtual UserModel user { get; set; }
        public virtual RoomModel room { get; set; }
    }
}
