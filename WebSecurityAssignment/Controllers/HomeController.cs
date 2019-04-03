using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Models;
using WebSecurityAssignment.Repositories;

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
            JobRepo jobRepo = new JobRepo(db);
            var jobs = jobRepo.GetAllJobs();        

            if (TempData["applicationMessage"] != null)
            {
                ViewBag.message = TempData["applicationMessage"];
            }

            return View(jobs);
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

        public IActionResult API()
        {
            ViewData["Message"] = "Your API page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
