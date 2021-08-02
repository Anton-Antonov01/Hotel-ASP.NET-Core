using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_BLL.Services;
using Hotel_PL.Models;
using Hotel_PL.RequestModels;
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
                        catch (NullReferenceException ex)
                        {
                            rm.Ignore();
                        }
                    });
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                    cfg.CreateMap<RoomRequest, RoomDTO>().ReverseMap();
                    cfg.CreateMap<CategoryRequest, CategoryDTO>();
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
            return View(mapper.Map<RoomDTO, RoomModel>(roomService.Get(id)));
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
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError("CategoryId", "Категории с таким Id не существует");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Name", "Комната с таким номером уже существует");
            }


            return View("CreateRoom",roomRequest);
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            var room = mapper.Map<RoomDTO, RoomRequest>(roomService.Get(id));
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
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError("CategoryId", "Категории с таким Id не существует");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Name", "Комната с таким номером уже существует");
            }

            return View("EditRoom", roomRequest);
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
                return View("Error");
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
