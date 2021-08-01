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
        public CategoryController(ICategoryService categoryService)
        {
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                    cfg.CreateMap<CategoryRequest, CategoryDTO>().ReverseMap();
                }).CreateMapper();

            this.categoryService = categoryService;
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
            var category = mapper.Map<CategoryDTO,CategoryModel>(categoryService.Get(id));
            return View(category);
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
                categoryService.AddCategory(mapper.Map<CategoryRequest, CategoryDTO>(categoryRequest));


                return RedirectToAction("AllCategories");
            }
            catch
            {
                return View("Error");
            }
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

                return RedirectToAction("AllCategories");
            }
            catch
            {
                return View("Error");
            }
        }

        //// GET: CategoryController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: CategoryController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                categoryService.DeleteCategory(id);
                return RedirectToAction("AllCategories");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
