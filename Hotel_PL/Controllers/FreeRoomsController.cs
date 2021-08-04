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

namespace Hotel_PL.Controllers
{
    [Authorize(Roles = "Admin, Guest")]
    public class FreeRoomsController : Controller
    {
        IRoomService roomService;
        IBaseService baseService;
        IPriceCategoryService priceCategoryService;
        IMapper mapper;

        public FreeRoomsController(IRoomService roomService, IBaseService baseService, IPriceCategoryService priceCategoryService)
        {
            this.roomService = roomService;
            this.baseService = baseService;
            this.priceCategoryService = priceCategoryService;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RoomDTO, RoomModel>().ForMember
                    ("Price", rm =>
                    {
                        try
                        {
                            rm.MapFrom(c => priceCategoryService.GetAllPriceCategories().
                                  SingleOrDefault(p => p.Category.Id == c.RoomCategory.Id && p.EndDate > DateTime.Now).Price);
                        }
                        catch (NullReferenceException ex)
                        {
                            rm.Ignore();
                        }
                    });
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                }).CreateMapper();
        }

        [HttpGet]
        public ActionResult FreeRoomsNow()
        {
            var rooms = baseService.FreeRoomsByDate(DateTime.Now);
            var RoomModels = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(rooms);
            return View("FreeRoomsNow",RoomModels);
        }

        [HttpGet]
        public ActionResult FreeRoomsByDate()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FreeRoomsByDateResult(DateTime date)
        {
            var rooms = baseService.FreeRoomsByDate(date);
            var RoomModels = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(rooms);
            return View(RoomModels);
        }

        [HttpGet]
        public ActionResult FreeRoomsByDateRange()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FreeRoomsByDateRangeResult(DateTime FirstDate, DateTime SecondDate)
        {
            var rooms = baseService.FreeRoomsByDateRange(FirstDate, SecondDate);
            var RoomModels = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(rooms);
            return View(RoomModels);
        }
    }
}
