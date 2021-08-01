using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.RequestModels
{
    public class CategoryRequest
    {
        [Required]
        [Display(Name = "Название категории")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Кровати")]
        public int Bed { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
