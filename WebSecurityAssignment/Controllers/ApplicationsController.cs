using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [HttpGet("Applications/")]
        public async Task<IActionResult> Index()
        {
            ApplicationRepo applicationRepo = new ApplicationRepo(_context);
            var applications = applicationRepo.GetAllApplications();

            return View(applications);
        }

        [HttpGet("Applications/Get/{jobID}")]
        public async Task<IActionResult> Index(int jobID)
        {
            ApplicationRepo applicationRepo = new ApplicationRepo(_context);
            string applicantID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applications = applicationRepo.GetAllApplications(applicantID, jobID);

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
        public IActionResult Create(int id)
        {
            JobRepo jobRepo = new JobRepo(_context);
            var job = jobRepo.GetJob(id);
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_context.Applications.Where(a => a.JobID == job.jobID && a.ApplicantID == currentUser).FirstOrDefault() == null)
            {
                var employee = _context.Users.Find(job.employeeID);
                var employer = _context.Users.Find(job.employerID);
                var address = _context.Addresses.Find(job.addressID);


                List<string> applicantID = new List<string>();
                applicantID.Add(currentUser);

                List<string> jobID = new List<string>();
                jobID.Add(id.ToString());

                ViewData["ApplicantID"] = new SelectList(applicantID);
                ViewData["JobID"] = new SelectList(jobID);
                ViewData["Title"] = job.title;
                ViewData["Employer"] = employer.FirstName + " " + employer.LastName;
                ViewData["JobDetail"] = job.description;
                ViewData["Address"] = (address.streetAddress + " " + address.city + " " + address.province + " " + address.postalCode);


                return View();
            }
            else
            {
                TempData["alreadyAppliedMessage"] = "You have already applied for this job.";
                return RedirectToAction(nameof(Index), "", new { areas = "" });
            }
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
                applicationRepo.CreateApplication(application.ApplicantID,application.JobID,application.Comment);
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

        [HttpGet]
//        [ValidateAntiForgeryToken]    
        public IActionResult Accept(int jobId, string acceptedApplicantId) {
            if (ModelState.IsValid)
            {
                string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                JobRepo jobRepo = new JobRepo(_context);
                
                var job = jobRepo.GetJob(jobId);
                if (currentUserId == job.employerID) {
                    jobRepo.AcceptApplcation(jobId, acceptedApplicantId);
                }
                //return RedirectToAction("Details/" + job.jobID, "Jobs", null);
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                ViewBag.Error = "An error occurred while updating this application. Please try again.";
                return RedirectToAction(nameof(Index));   
            }
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

            //who the f left all this here?
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
