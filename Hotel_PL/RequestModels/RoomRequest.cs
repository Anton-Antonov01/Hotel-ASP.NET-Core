using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.RequestModels
{
    public class RoomRequest
    {

        [Required]
        [Display(Name = "Номер комнаты")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Id категории")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "В активе")]
        public bool Active { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, CategoryId {CategoryId}, Active {Active}";
        }

    }
}
