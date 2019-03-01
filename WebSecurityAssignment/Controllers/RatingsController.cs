using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Repositories;

namespace WebSecurityAssignment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ratings
        public IActionResult Index()
        {
            RatingsRepo ratingsRepo = new RatingsRepo(_context);
            return View(ratingsRepo.GetAllRatings());
        }

        // GET: Ratings/Details/5
        public IActionResult Details(string employeeID, int jobID)
        {
            RatingsRepo ratingsRepo = new RatingsRepo(_context);
            Ratings ratings = ratingsRepo.GetRating(employeeID, jobID);
            return View(ratings);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratings = await _context.Ratings.FindAsync(id);
            if (ratings == null)
            {
                return NotFound();
            }
            ViewData["employeeID"] = new SelectList(_context.Users, "Id", "Id", ratings.employeeID);
            return View(ratings);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewBag.Error = "An error occurred while updating this rating. Please try again.";
            return View();
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(string employeeID, int jobID)
        {
            if (employeeID == null || jobID < 0)
            {
                return NotFound();
            }

            var ratings = await _context.Ratings
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(m => m.employeeID == employeeID && m.jobID == jobID);
            if (ratings == null)
            {
                return NotFound();
            }

            return View(ratings);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Ratings rating)
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
            ViewBag.Error = "An error occurred while deleting this rating. Please try again.";
            return View();
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["employeeID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["jobID"] = new SelectList(_context.Jobs, "jobID", "jobID");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ratings rating)
        {
            var ratings = _context.Ratings;
            if (ModelState.IsValid)
            {
                RatingsRepo ratingsRepo = new RatingsRepo(_context);
                var success = ratingsRepo.CreateRating(rating.jobID, rating.employeeID, rating.review, rating.score);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Error = "An error occurred while creating this rating. Please try again.";
            return View();
        }

        private bool RatingsExists(string id)
        {
            return _context.Ratings.Any(e => e.employeeID == id);
        }
    }
}
