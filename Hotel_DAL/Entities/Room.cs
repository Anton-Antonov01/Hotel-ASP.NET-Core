using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel_DAL.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CategoryId")]
        public Category RoomCategory { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Room)
            {
                var objRM = obj as Room;
                return this.Id == objRM.Id
                    && this.Name == objRM.Name
                    //&& this.RoomCategory.Equals(objRM.RoomCategory)
                    && this.Active == objRM.Active;
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
