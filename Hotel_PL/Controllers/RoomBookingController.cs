using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_PL.Models;
using Hotel_PL.RequestModels;
using Hotel_PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Controllers
{
    [Authorize(Roles = "Guest, Admin")]
    public class RoomBookingController : Controller
    {
        IMapper mapper;
        IBookingService bookingService;
        IRoomService roomService;
        IUserService userService;
        IPriceCategoryService priceCategoryService;

        public RoomBookingController(IBookingService bookingService, IRoomService roomService, IUserService userService, IPriceCategoryService priceCategoryService)
        {
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RoomBookingRequest, BookingDTO>().ReverseMap();
                    cfg.CreateMap<RoomDTO, RoomModel>();
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                    cfg.CreateMap<RoomBookingViewRequest, BookingDTO>();
                }).CreateMapper();

            this.bookingService = bookingService;
            this.userService = userService;
            this.roomService = roomService;
            this.priceCategoryService = priceCategoryService;

        }


        [HttpGet]
        public ActionResult DoBooking(int RoomId)
        {
            var roomNum = roomService.Get(RoomId).Name;
            var categoryName = roomService.Get(RoomId).RoomCategory.Name;
            var pricePerDay = priceCategoryService.GetAllPriceCategories().SingleOrDefault(pc => pc.Category.Id == roomService.Get(RoomId).RoomCategory.Id && pc.EndDate >= DateTime.Now).Price;
            var roomBookingViewRequest = new RoomBookingViewRequest() { 
                roomId = RoomId,
                roomBookingRequest = new RoomBookingRequest() { BookingDate = DateTime.Now, EnterDate = DateTime.Now, NumberOfDays = 1},
                roomNumber = roomNum , category = categoryName, PricePerDay = pricePerDay };


            return View("DoBooking", roomBookingViewRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoBooking(RoomBookingViewRequest roomBookingViewRequest)
        {

            if(ModelState.IsValid)
            {
                try
                {
                    BookingDTO bookingDTO = mapper.Map<RoomBookingViewRequest, BookingDTO>(roomBookingViewRequest);
                    bookingDTO.LeaveDate = bookingDTO.EnterDate.AddDays(roomBookingViewRequest.roomBookingRequest.NumberOfDays);
                    bookingDTO.user = userService.GetByPhoneNumber(User.Identity.Name);
                    bookingDTO.room = roomService.Get(roomBookingViewRequest.roomId);
             
             
                    bookingService.AddBooking(bookingDTO);
             
                    return RedirectToAction("AllRooms","Room");
                }
                catch
                {
                    return RedirectToAction("AllRooms", "Room");
                }
            }
            else
            {
                
            }
            return View(roomBookingViewRequest);
        }
    }
}
