using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProfitController : Controller
    {
        IBaseService baseService;
        IMapper mapper;
        public ProfitController(IBaseService baseService)
        {
            this.baseService = baseService;
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<ProfitByMonthDTO, ProfitByMonthModel>();
                }).CreateMapper();
        }

        [HttpGet]
        public ActionResult ProfitByMonths()
        {
            var profit = baseService.GetProfitByMonths();
            var profitModel = mapper.Map<IEnumerable<ProfitByMonthDTO>, IEnumerable<ProfitByMonthModel>>(profit);
            return View(profitModel);
        }

        [HttpGet]
        public ActionResult ProfitByMonth()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ProfitByMonthResult(DateTime month)
        {
            var profitByMonth = baseService.GetProfitByOneMonth(month);
            var profitByMonthModel = mapper.Map<ProfitByMonthDTO, ProfitByMonthModel>(profitByMonth);
            return View(profitByMonthModel);
        }
    }
}
