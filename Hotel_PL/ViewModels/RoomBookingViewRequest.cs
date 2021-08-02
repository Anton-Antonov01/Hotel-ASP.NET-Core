using Hotel_PL.RequestModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.ViewModels
{
    public class RoomBookingViewRequest : IValidatableObject
    {
        public RoomBookingRequest roomBookingRequest {get; set;}

        public int roomId { get; set; }

        public string roomNumber { get; set; }

        public string category { get; set; }

        public decimal PricePerDay { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (roomBookingRequest.BookingDate > roomBookingRequest.EnterDate)
            {
                errors.Add(new ValidationResult("Дата бронирования должна быть раньше, чем дата въезда!"));
            }
            if (roomBookingRequest.NumberOfDays < 1)
            {
                errors.Add(new ValidationResult("Колличество дней должно быть больше нуля"));
            }
            if(roomBookingRequest.BookingDate.Date < DateTime.Now.Date || roomBookingRequest.EnterDate.Date < DateTime.Now.Date)
            {
                errors.Add(new ValidationResult("Нельзя указывать даты из прошлого"));
            }

            return errors;
        }
    }
}
