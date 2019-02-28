using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Repositories;

namespace WebSecurityAssignment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RatingsController : Controller
    {
        ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Role
        public ActionResult Index()
        {
            RatingsRepo ratingsRepo = new RatingsRepo(_context);
            return View(ratingsRepo.GetAllRatings());
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Ratings rating)
        {
            var ratings = _context.Ratings;
            if (ModelState.IsValid)
            {
                RatingsRepo ratingsRepo = new RatingsRepo(_context);
                var success = ratingsRepo.RemoveRating(rating.employeeID, rating.jobID);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Error = "An error occurred while deleting this role. Please try again.";
            return View();
        }

        [HttpPut]
        public ActionResult Update(Ratings rating)
        {
            var ratings = _context.Ratings;
            if (ModelState.IsValid)
            {
                RatingsRepo ratingsRepo = new RatingsRepo(_context);
                var success = ratingsRepo.UpdateRating(rating.jobID, rating.employeeID, rating.review, rating.score);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Error = "An error occurred while deleting this role. Please try again.";
            return View();
        }
    }
}