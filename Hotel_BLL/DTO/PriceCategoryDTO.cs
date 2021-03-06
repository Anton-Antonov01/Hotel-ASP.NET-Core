using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.DTO
{
    public class PriceCategoryDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CategoryDTO Category { get; set; }

        public override string ToString()
        {
            return $"Price: {Price}, StartDate: {StartDate}, EndDate: {EndDate}, CategoryId: {Category.Id}";
        }
    }
}
