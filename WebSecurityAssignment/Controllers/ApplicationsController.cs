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
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            ApplicationRepo applicationRepo = new ApplicationRepo(_context);
            var applications = applicationRepo.GetAllApplications();

            return View(applications);
        }

        // GET: Applications/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var application = await _context.Applications
        //        .Include(a => a.ApplicationUser)
        //        .Include(a => a.Job)
        //        .FirstOrDefaultAsync(m => m.ApplicantID == id);
        //    if (application == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(application);
        //}

        public async Task<IActionResult> Detail(string applicantID, int jobID)
        {
            ApplicationRepo applicationRepo = new ApplicationRepo(_context);

            if (applicantID == null || jobID == 0)
            {
                return NotFound();
            }

            var application = applicationRepo.GetApplication(applicantID, jobID);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["ApplicantID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["JobID"] = new SelectList(_context.Jobs, "jobID", "jobID");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantID,JobID,Comment")] Application application)
        {
            ApplicationRepo applicationRepo = new ApplicationRepo(_context);

            if (ModelState.IsValid)
            {
                //_context.Add(application);
                //await _context.SaveChangesAsync();
                applicationRepo.CreateApplication(application.ApplicantID,application.JobID);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantID"] = new SelectList(_context.Users, "Id", "Id", application.ApplicantID);
            ViewData["JobID"] = new SelectList(_context.Jobs, "jobID", "jobID", application.JobID);
            return View(application);
        }

        public async Task<IActionResult> Edit(string applicantID, int jobID)
        {
            ApplicationRepo applicationRepo = new ApplicationRepo(_context);

            if (applicantID == null || jobID == 0)
            {
                return NotFound();
            }

            var application = applicationRepo.GetApplication(applicantID, jobID);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ApplicantID,JobID,Comment")] Application application)
        {
            if (ModelState.IsValid)
            {
                ApplicationRepo applicationRepo = new ApplicationRepo(_context);
                applicationRepo.UpdateApplication(application);
            }

            ViewBag.Error = "An error occurred while updating this application. Please try again.";
            return RedirectToAction(nameof(Index));

            //if (id != application.ApplicantID)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(application);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ApplicationExists(application.ApplicantID, application.JobID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ApplicantID"] = new SelectList(_context.Users, "Id", "Id", application.ApplicantID);
            //ViewData["JobID"] = new SelectList(_context.Jobs, "jobID", "jobID", application.JobID);
            //return View(application);
        }

        // GET: Applications/Delete
        public async Task<IActionResult> Delete(string applicantID, int jobID)
        {
            ApplicationRepo applicationRepo = new ApplicationRepo(_context);

            if (applicantID == null || jobID == 0)
            {
                return NotFound();
            }

           var application = applicationRepo.GetApplication(applicantID, jobID);

           if (application == null)
           {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string applicantID, int jobID)
        {
            if (ModelState.IsValid)
            {
                ApplicationRepo applicationRepo = new ApplicationRepo(_context);
                applicationRepo.DeleteApplication(applicantID, jobID);
            }

            ViewBag.Error = "An error occurred while deleting this application. Please try again.";
            return RedirectToAction(nameof(Index));
        }

     
        private bool ApplicationExists(string applicantID, int jobID)
        {
            return _context.Applications.Any(a => a.ApplicantID == applicantID && a.JobID == jobID);
        }

    }
}
