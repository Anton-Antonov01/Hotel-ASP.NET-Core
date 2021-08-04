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
    public class CategoryController : Controller
    {
        IMapper mapper;
        ICategoryService categoryService;
        ILogService logService;
        IUserService userService;
        public CategoryController(ICategoryService categoryService, ILogService logService, IUserService userService)
        {
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                    cfg.CreateMap<CategoryRequest, CategoryDTO>().ReverseMap();
                    cfg.CreateMap<LogDataModel, LogDataDTO>();
                }).CreateMapper();

            this.categoryService = categoryService;
            this.logService = logService;
            this.userService = userService;
        }


        // GET: CategoryController
        [HttpGet]
        public ActionResult AllCategories()
        {
            var Categories = categoryService.GetAllCategories();
            return View(mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(Categories));
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var category = mapper.Map<CategoryDTO, CategoryModel>(categoryService.Get(id));
                return View(category);
            }
            catch
            {
                var Categories = categoryService.GetAllCategories();
                return View("AllCategories", mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(Categories));
            }
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View("CreateCategory");
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryRequest categoryRequest)
        {
            try
            {
                var categoryId = categoryService.AddCategory(mapper.Map<CategoryRequest, CategoryDTO>(categoryRequest));
                var adminId = userService.GetByPhoneNumber(User.Identity.Name).Id;

                LogDataModel logDataModel = new LogDataModel()
                {
                    AdminId = adminId,
                    EntityId = categoryId,
                    EntityName = "Category",
                    ObjectState = categoryRequest.ToString(),
                };

                logService.AddCreateLog(mapper.Map<LogDataModel,LogDataDTO>(logDataModel));

                return RedirectToAction("AllCategories");
            }
            catch(ArgumentException)
            {
                ModelState.AddModelError("Name", "Категория с таким названием и таким же колличеством кроватей уже существует");
            }

            return View("CreateCategory", categoryRequest);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = mapper.Map<CategoryDTO, CategoryRequest>(categoryService.Get(id));
            return View("EditCategory", category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryRequest categoryRequest)
        {
            try
            {
                var categoryDTO = mapper.Map<CategoryRequest, CategoryDTO>(categoryRequest);
                categoryDTO.Id = id;

                categoryService.UpdateCategory(categoryDTO);

                var adminId = userService.GetByPhoneNumber(User.Identity.Name).Id;

                LogDataModel logDataModel = new LogDataModel()
                {
                    AdminId = adminId,
                    EntityId = id,
                    EntityName = "Category",
                    ObjectState = categoryRequest.ToString(),
                };

                logService.AddEditLog(mapper.Map<LogDataModel, LogDataDTO>(logDataModel));



                return RedirectToAction("AllCategories");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Name", "Категория с таким названием и таким же колличеством кроватей уже существует");
            }

            return View("EditCategory", categoryRequest);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var Category = categoryService.Get(id);
                categoryService.DeleteCategory(id);


                var adminId = userService.GetByPhoneNumber(User.Identity.Name).Id;


                LogDataModel logDataModel = new LogDataModel()
                {
                    AdminId = adminId,
                    EntityId = id,
                    EntityName = "Category",
                    ObjectState = Category.ToString(),
                };

                logService.AddDeleteLog(mapper.Map<LogDataModel, LogDataDTO>(logDataModel));

                return RedirectToAction("AllCategories");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
