using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.RequestModels
{
    public class PriceCategoryRequest
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
    }
}
