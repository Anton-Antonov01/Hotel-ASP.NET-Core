using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Hotel_DAL.Entities
{
    public class User: IdentityUser<int>
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }



        //public Role UserRole { get; set; } 
        public ICollection<Booking> Bookings { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                var objRM = obj as User;
                return this.Id == objRM.Id
                    && this.Name == objRM.Name
                    && this.Address == objRM.Address
                    && this.PhoneNumber == objRM.PhoneNumber;
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
