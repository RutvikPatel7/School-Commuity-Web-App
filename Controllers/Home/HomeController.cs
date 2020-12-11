using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Data;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public HomeController(SchoolCommunityContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
