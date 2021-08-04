using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.RequestModels
{
    public class BookingRequest : IValidatableObject
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
        [Display(Name = "Заселение")]
        public bool Set { get; set; }

        [Required]
        [Display(Name = "Id комнаты")]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Id пользователя")]
        public int UserId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (BookingDate > EnterDate || EnterDate > LeaveDate || BookingDate > LeaveDate)
            {
                errors.Add(new ValidationResult("Дата бронирования должна быть раньше, чем дата въезда"));
            }

            return errors;
        }

        public override string ToString()
        {
            return $"BookingDate: {BookingDate}, EnterDate: {EnterDate}, LeaveDate: {LeaveDate}, Set: {Set}, RoomId: {RoomId}, UserId: {UserId}";
        }

    }

}
