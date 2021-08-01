using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }
    }
}
