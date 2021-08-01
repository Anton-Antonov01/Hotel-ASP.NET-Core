using Microsoft.AspNetCore.Identity;
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
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public bool Set { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }

        [ForeignKey("RoomId")]
        public  Room room { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Booking)
            {
                var objCM = obj as Booking;
                return this.Id == objCM.Id
                    && this.BookingDate == objCM.BookingDate
                    && this.EnterDate == objCM.EnterDate
                    && this.LeaveDate == objCM.LeaveDate
                    && this.Set == objCM.Set; //Добавить сравнение комнаты и гостя
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
