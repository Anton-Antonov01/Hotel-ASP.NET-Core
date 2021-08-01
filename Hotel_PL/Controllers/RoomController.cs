using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_BLL.Services;
using Hotel_PL.Models;
using Hotel_PL.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Controllers
{
    public class RoomController : Controller
    {
        private IRoomService roomService;
        private ICategoryService categoryService;
        private IPriceCategoryService priceCategoryService;

        private IMapper mapper; 
        public RoomController(IRoomService roomService, ICategoryService categoryService, IPriceCategoryService priceCategoryService)
        {
            this.categoryService = categoryService;
            this.roomService = roomService;
            this.priceCategoryService = priceCategoryService;

            mapper = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<RoomDTO, RoomModel>().ForMember
                    ("Price", rm => {
                        try
                        {
                            rm.MapFrom(c => priceCategoryService.GetAllPriceCategories().
                                  SingleOrDefault(p => p.Category.Id == c.RoomCategory.Id && p.EndDate > DateTime.Now).Price);
                        }
                        catch(NullReferenceException ex)
                        {
                            rm.Ignore();
                        }
                    });
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                    cfg.CreateMap<RoomRequest, RoomDTO>().ReverseMap();
                    cfg.CreateMap<CategoryRequest, CategoryDTO>();
                    cfg.CreateMap<PriceCategoryDTO, PriceCategoryService>();
                }
                ).CreateMapper();

        }
        // GET: RoomController
        [HttpGet]
        public ActionResult AllRooms()
        {
            var roomModels = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(roomService.GetAllRooms());


            return View(roomModels);
        }

        // GET: RoomController/Details/5
        public ActionResult Details(int id)
        {
            var roomModel = mapper.Map<RoomDTO, RoomModel>(roomService.Get(id));
            var prcieCat = priceCategoryService.GetAllPriceCategories().SingleOrDefault(p => p.Category.Id == roomModel.RoomCategory.Id && p.EndDate > DateTime.Now);
            if (prcieCat != null)
                roomModel.Price = prcieCat.Price;
            else
                roomModel.Price = -1;

            return View(roomModel);
        }

        // GET: RoomController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateRoom");
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomRequest roomRequest)
        {
            try
            {
                var roomDTO = mapper.Map<RoomRequest, RoomDTO>(roomRequest);
                roomDTO.RoomCategory = categoryService.Get(roomRequest.CategoryId);
                roomService.AddRoom(roomDTO);

                return RedirectToAction("AllRooms");
            }
            catch
            {
                return RedirectToAction("AllRooms");
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            var room = mapper.Map<RoomDTO,RoomRequest>(roomService.Get(id));
            room.CategoryId = roomService.Get(id).RoomCategory.Id;

            return View("EditRoom", room);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RoomRequest roomRequest)
        {
            try
            {
                var roomDTO = mapper.Map<RoomRequest, RoomDTO>(roomRequest);
                roomDTO.RoomCategory = categoryService.Get(roomRequest.CategoryId);
                roomDTO.Id = id;
                roomService.UpdateRoom(roomDTO);

                return RedirectToAction("AllRooms");
            }
            catch
            {
                return RedirectToAction("AllRooms");
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                roomService.DeleteRoom(id);
                return RedirectToAction("AllRooms");
            }
            catch
            {
                return RedirectToAction("AllRooms");
            }
        }

        // POST: RoomController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
