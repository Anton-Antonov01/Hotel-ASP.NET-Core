using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Bed { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Bed: {Bed}, Description: {Description}";
        }
    }
}
