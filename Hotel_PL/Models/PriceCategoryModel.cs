using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Models
{
    public class PriceCategoryModel
    {

        public int Id { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Дата начала действия")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата конца действия")]
        public DateTime EndDate { get; set; }
        public CategoryModel Category { get; set; }
    }
}
