using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_DAL.Entities;
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
    public class LogController : Controller
    {
        ILogService logService;
        IMapper mapper;

        public LogController(ILogService logService)
        {
            this.logService = logService;
            mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<LogDTO, LogModel>()
                ).CreateMapper();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var logs = mapper.Map<IEnumerable<LogDTO>, IEnumerable<LogModel>>(logService.GetAll().Reverse());
            return View(logs);
        }

    }
}
