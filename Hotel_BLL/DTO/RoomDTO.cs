using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryDTO RoomCategory { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, CategoryId {RoomCategory.Id}, Active {Active}";
        }

    }
}
