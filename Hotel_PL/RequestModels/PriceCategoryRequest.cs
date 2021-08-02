using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.RequestModels
{
    public class PriceCategoryRequest : IValidatableObject
    {
        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Дата конца")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Id категории")]
        public int CategoryId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (StartDate > EndDate)
            {
                errors.Add(new ValidationResult("Дата начала должна быть раньше, чем дата конца!"));
            }
            if (Price < 1)
            {
                errors.Add(new ValidationResult("Цена не может равняться нулю и меньше"));
            }

            return errors;
        }


    }
}
