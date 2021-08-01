using AutoMapper;
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
    public class PriceCategoryController : Controller
    {
        IMapper mapper;
        IPriceCategoryService priceCategoryService;
        ICategoryService categoryService;

        public PriceCategoryController(IPriceCategoryService priceCategoryService, ICategoryService categoryService)
        {
            this.priceCategoryService = priceCategoryService;
            this.categoryService = categoryService;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>();
                    cfg.CreateMap<PriceCategoryDTO, PriceCategoryRequest>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                }).CreateMapper();
        }



        // GET: PriceCategoryController
        public ActionResult AllPriceCategories()
        {
            var priceCategories = mapper.Map<IEnumerable<PriceCategoryDTO>, IEnumerable<PriceCategoryModel>>(priceCategoryService.GetAllPriceCategories());
            return View(priceCategories);
        }

        // GET: PriceCategoryController/Details/5
        public ActionResult Details(int id)
        {
            var priceCategory = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryService.Get(id));
            return View(priceCategory);
        }

        // GET: PriceCategoryController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PriceCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PriceCategoryRequest priceCategoryRequest)
        {
            try
            {
                var PriceCatDTO = mapper.Map<PriceCategoryRequest, PriceCategoryDTO>(priceCategoryRequest);
                PriceCatDTO.Category = categoryService.Get(priceCategoryRequest.CategoryId);

                priceCategoryService.AddPriceCategory(PriceCatDTO);

                return RedirectToAction("AllPriceCategories");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: PriceCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(mapper.Map<PriceCategoryDTO, PriceCategoryRequest>(priceCategoryService.Get(id)));
        }

        // POST: PriceCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PriceCategoryRequest priceCategoryRequest)
        {
            try
            {
                var PriceCatDTO = mapper.Map<PriceCategoryRequest, PriceCategoryDTO>(priceCategoryRequest);
                PriceCatDTO.Category = categoryService.Get(priceCategoryRequest.CategoryId);
                PriceCatDTO.Id = id;
                priceCategoryService.UpdatePriceCategory(PriceCatDTO);

                return RedirectToAction("AllPriceCategories");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            priceCategoryService.DeletePriceCategory(id);
            return RedirectToAction("AllPriceCategories");
        }
    }
}
