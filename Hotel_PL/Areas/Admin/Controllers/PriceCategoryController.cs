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
        IUserService userService;
        ILogService logService;

        public PriceCategoryController(IPriceCategoryService priceCategoryService, ICategoryService categoryService, IUserService userService, ILogService logService)
        {
            this.logService = logService;
            this.priceCategoryService = priceCategoryService;
            this.categoryService = categoryService;
            this.userService = userService;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>();
                    cfg.CreateMap<PriceCategoryDTO, PriceCategoryRequest>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                    cfg.CreateMap<LogDataModel, LogDataDTO>();
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
            try
            {
                var priceCategory = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryService.Get(id));
                return View(priceCategory);
            }
            catch
            {
                var priceCategories = mapper.Map<IEnumerable<PriceCategoryDTO>, IEnumerable<PriceCategoryModel>>(priceCategoryService.GetAllPriceCategories());
                return View("AllPriceCategories",priceCategories);
            }

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
            if (ModelState.IsValid)
            {
                try
                {
                    var PriceCatDTO = mapper.Map<PriceCategoryRequest, PriceCategoryDTO>(priceCategoryRequest);
                    PriceCatDTO.Category = categoryService.Get(priceCategoryRequest.CategoryId);

                    var priceCatId = priceCategoryService.AddPriceCategory(PriceCatDTO);


                    var adminId = userService.GetByPhoneNumber(User.Identity.Name).Id;

                    LogDataModel logDataModel = new LogDataModel()
                    {
                        AdminId = adminId,
                        EntityId = priceCatId,
                        EntityName = "PriceCategory",
                        ObjectState = priceCategoryRequest.ToString(),
                    };

                    logService.AddCreateLog(mapper.Map<LogDataModel, LogDataDTO>(logDataModel));



                    return RedirectToAction("AllPriceCategories");
                }
                catch (ArgumentNullException)
                {
                    ModelState.AddModelError("CategoryId", "Такой категории не существует");
                }
                catch (ArgumentException)
                {
                    ModelState.AddModelError("StartDate", "Даты действия цены для категории пересекаются с уже сущесвующей");
                }

                return View(priceCategoryRequest);
            }
            else
            {
                return View(priceCategoryRequest);
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
            if (ModelState.IsValid)
            {
                try
                {
                    var PriceCatDTO = mapper.Map<PriceCategoryRequest, PriceCategoryDTO>(priceCategoryRequest);
                    PriceCatDTO.Category = categoryService.Get(priceCategoryRequest.CategoryId);
                    PriceCatDTO.Id = id;
                    priceCategoryService.UpdatePriceCategory(PriceCatDTO);

                    var adminId = userService.GetByPhoneNumber(User.Identity.Name).Id;

                    LogDataModel logDataModel = new LogDataModel()
                    {
                        AdminId = adminId,
                        EntityId = id,
                        EntityName = "PriceCategory",
                        ObjectState = priceCategoryRequest.ToString(),
                    };

                    logService.AddEditLog(mapper.Map<LogDataModel, LogDataDTO>(logDataModel));


                    return RedirectToAction("AllPriceCategories");
                }
                catch (ArgumentNullException)
                {
                    ModelState.AddModelError("CategoryId", "Такой категории не существует");
                }
                catch (ArgumentException)
                {
                    ModelState.AddModelError("StartDate", "Даты действия цены для категории пересекаются с уже сущесвующей");
                }
                return View(priceCategoryRequest);
            }
            else
            {
                return View(priceCategoryRequest);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var priceCat = priceCategoryService.Get(id);
            priceCategoryService.DeletePriceCategory(id);


            var adminId = userService.GetByPhoneNumber(User.Identity.Name).Id;

            LogDataModel logDataModel = new LogDataModel()
            {
                AdminId = adminId,
                EntityId = id,
                EntityName = "PriceCategory",
                ObjectState = priceCat.ToString(),
            };

            logService.AddDeleteLog(mapper.Map<LogDataModel, LogDataDTO>(logDataModel));


            return RedirectToAction("AllPriceCategories");
        }
    }
}
