using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Models
{
    public class RoomModel
    {
        [Display(Name = "Id комнаты")]
        public int Id { get; set; }

        [Display(Name = "Комната")]
        public string Name { get; set; }

        public CategoryModel RoomCategory { get; set; }

        [Display(Name = "В активе")]
        public bool Active { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
