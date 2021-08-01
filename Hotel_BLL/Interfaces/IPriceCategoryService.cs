using Hotel_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.Interfaces
{
    public interface IPriceCategoryService
    {
        PriceCategoryDTO Get(int id);
        void AddPriceCategory(PriceCategoryDTO guestDTO);
        IEnumerable<PriceCategoryDTO> GetAllPriceCategories();
        void DeletePriceCategory(int id);
        void UpdatePriceCategory(PriceCategoryDTO priceCategoryDTO);
    }
}
