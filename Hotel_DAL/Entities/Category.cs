﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel_DAL.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Bed { get; set; }
        public string Description { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public ICollection<PriceCategory> PriceCategories { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Category)
            {
                var objCM = obj as Category;
                return this.Id == objCM.Id
                    && this.Name == objCM.Name
                    && this.Bed == objCM.Bed;
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
