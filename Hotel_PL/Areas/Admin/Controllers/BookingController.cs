﻿using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
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
    public class BookingController : Controller
    {
        IMapper mapper;
        IBookingService bookingService;
        IRoomService roomService;
        IUserService guestService;
        IPriceCategoryService priceCategoryService;


        public BookingController(IBookingService bookingService, IRoomService roomService, IUserService guestService, IPriceCategoryService priceCategoryService)
        {
            this.bookingService = bookingService;
            this.guestService = guestService;
            this.roomService = roomService;
            this.priceCategoryService = priceCategoryService;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<BookingRequest, BookingDTO>().ReverseMap().ForMember("UserId", bdto=> bdto.MapFrom(x => x.user.Id));


                    cfg.CreateMap<BookingDTO, BookingModel>().ForMember("TotalPrice", bm=>
                    {
                        bm.MapFrom(x => bookingService.GetTotalPrice(x.Id));      
                    });
                    cfg.CreateMap<UserDTO, UserModel>();
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
                }).CreateMapper();
        }


        // GET: BookingController
        public ActionResult AllBookings()
        {
            var bookingDTOs = bookingService.GetAllBookings();
            var bookingModels = mapper.Map<IEnumerable<BookingDTO>, IEnumerable<BookingModel>>(bookingDTOs);
            return View(bookingModels);
        }


        public ActionResult CheckInTodayBookings()
        {
            var bookingDTOs = bookingService.GetAllBookings();
            var bookingModels = mapper.Map<IEnumerable<BookingDTO>, IEnumerable<BookingModel>>(bookingDTOs).Where(b=> b.EnterDate.Date == DateTime.Now.Date);

            return View("AllBookings",bookingModels);
        }

        // GET: BookingController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var bookingDTO = bookingService.Get(id);
                var bookingmodel = mapper.Map<BookingDTO, BookingModel>(bookingDTO);
                return View("BookingDetails", bookingmodel);
            }
            catch
            {
                var bookingDTOs = bookingService.GetAllBookings();
                var bookingModels = mapper.Map<IEnumerable<BookingDTO>, IEnumerable<BookingModel>>(bookingDTOs);
                return View("AllBookings",bookingModels);
            }


        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(BookingRequest bookingRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bookingDTO = mapper.Map<BookingRequest, BookingDTO>(bookingRequest);
                    bookingDTO.room = roomService.Get(bookingRequest.RoomId);
                    bookingDTO.user = guestService.Get(bookingRequest.UserId);

                    bookingService.AddBooking(bookingDTO);
                    return RedirectToAction("AllBookings");

                }
                catch (NullReferenceException)
                {
                    ModelState.AddModelError("RoomId", "Пользователя или комнаты с таким Id не существует");
                }
                catch (ArgumentNullException)
                {
                    ModelState.AddModelError("RoomId", "Пользователя или комнаты с таким Id не существует");
                }
                catch (ArgumentException)
                {
                    ModelState.AddModelError("RoomId", "Для этой комнаты уже существует бронь на эти даты");
                }


                return View(bookingRequest);

            }
            else
            {
                return View(bookingRequest);
            }
        }

        // GET: BookingController/Edit/5
        public ActionResult Edit(int id)
        {
            var booking = mapper.Map<BookingDTO, BookingRequest>(bookingService.Get(id));

            return View("EditBooking", booking);
        }

        // POST: BookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookingRequest bookingRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bookingDTO = mapper.Map<BookingRequest, BookingDTO>(bookingRequest);
                    bookingDTO.Id = id;
                    bookingDTO.room = roomService.Get(bookingRequest.RoomId);
                    bookingDTO.user = guestService.Get(bookingRequest.UserId);

                    bookingService.UpdateBooking(bookingDTO);

                    return RedirectToAction("AllBookings");
                }
                catch (NullReferenceException)
                {
                    ModelState.AddModelError("RoomId", "Пользователя или комнаты с таким Id не существует");
                }
                catch (ArgumentNullException)
                {
                    ModelState.AddModelError("RoomId", "Пользователя или комнаты с таким Id не существует");
                }
                catch (ArgumentException)
                {
                    ModelState.AddModelError("RoomId", "Для этой комнаты уже существует бронь на эти даты");
                }
                return View("EditBooking", bookingRequest);
            }
            else
            {
                return View("EditBooking", bookingRequest);
            }
        }

        // GET: BookingController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                bookingService.DeleteBooking(id);
                return RedirectToAction("AllBookings");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
