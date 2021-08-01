using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Models
{
    public class CategoryModel
    {
        [Display(Name = "Id категории")]
        public int Id { get; set; }

        [Display(Name = "Название категории")]
        public string Name { get; set; }

        [Display(Name = "Кровати")]
        public int Bed { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
