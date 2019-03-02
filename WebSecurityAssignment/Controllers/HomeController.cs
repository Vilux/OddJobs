using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Models;

namespace WebSecurityAssignment.Controllers
{
	public class HomeController : Controller
	{
        private ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
            Seeder seeder = new Seeder(db);
        }

        public IActionResult Index()
		{
            var applicationDbContext = db.Jobs.Include(j => j.Address).Include(j => j.ApplicationUser);       

            return View(applicationDbContext.ToList());
        }

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
