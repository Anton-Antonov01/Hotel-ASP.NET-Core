using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Models
{
    public class ProfitByMonthModel
    {
        [Display(Name = "Прибыль за месяц")]
        public decimal Profit { get; set; }
        [Display(Name = "Месяц")]
        public string Month { get; set; }
    }
}
