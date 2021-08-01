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
    public class UserController : Controller
    {
        IMapper mapper;
        IUserService guestService;

        public UserController(IUserService guestService)
        {
            this.guestService = guestService;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<UserDTO, UserModel>();
                    cfg.CreateMap<UserRequest, UserDTO>().ReverseMap();
                }).CreateMapper();
        }




        // GET: GuestController
        public ActionResult AllGuests()
        {
            var guestModels = mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserModel>>(guestService.GetAllGuests());
            return View(guestModels);
        }

        // GET: GuestController/Details/5
        public ActionResult Details(int id)
        {
            var guestModel = mapper.Map<UserDTO, UserModel>(guestService.Get(id));
            return View("GuestDetails", guestModel);
        }

        // GET: GuestController/Create
        public ActionResult Create()
        {
            return View("CreateGuest");
        }

        // POST: GuestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRequest guestRequest)
        {
            try
            {
                var guestDTO = mapper.Map<UserRequest, UserDTO>(guestRequest);
                guestService.AddGuest(guestDTO);

                return RedirectToAction("AllGuests");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: GuestController/Edit/5
        public ActionResult Edit(int id)
        {
            var guestrequest = mapper.Map<UserDTO, UserRequest>(guestService.Get(id));
            return View("EditGuest", guestrequest);
        }

        // POST: GuestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserRequest guestRequest)
        {
            try
            {
                var guestDTO = mapper.Map<UserRequest, UserDTO>(guestRequest);
                guestDTO.Id = id;
                guestService.UpdateGuest(guestDTO);
                return RedirectToAction("AllGuests");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: GuestController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                guestService.DeleteGuest(id);
                return RedirectToAction("AllGuests");
            }
            catch
            {
                return View("Error");
            }
        }

    }
}