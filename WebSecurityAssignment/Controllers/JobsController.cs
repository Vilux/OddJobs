using System;
using System.Collections;
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
using WebSecurityAssignment.ViewModels;

namespace WebSecurityAssignment.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public IActionResult Index()
        {
            JobRepo jobRepo = new JobRepo(_context);
            var jobs = jobRepo.GetAllJobs();

            return View(jobs);
        }


        // GET: Jobs/Details/5
        public IActionResult Details(int id)
        {
            JobRepo jobRepo = new JobRepo(_context);
            var job = jobRepo.GetJob(id);

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {

            List<string> id = new List<string>();
            id.Add(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            ViewData["addressID"] = new SelectList(_context.Addresses, "addressID", "addressID");
            ViewData["employerID"] = new SelectList(id);
            JobCreateVM jobCreateVM = new JobCreateVM();

            jobCreateVM.dateNeeded = DateTime.Now;
            jobCreateVM.dateExpired = DateTime.Now.AddDays(30);
            return View(jobCreateVM);
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Amount,dateNeeded,dateExpired,Address,City,Province")] JobCreateVM jobCreateVM)
        {
            JobRepo jobRepo = new JobRepo(_context);

            Job job = new Job();

            if (ModelState.IsValid)
            {
                int addressId = 0;
                bool matchedAddress = false;

                foreach (Address address in _context.Addresses)
                {
                    if (address.streetAddress == jobCreateVM.Address &&
                        address.city == jobCreateVM.City &&
                        address.province == jobCreateVM.Province)
                    {
                        addressId = address.addressID;
                        matchedAddress = true;
                    }
                }
                if (!matchedAddress)
                {
                    AddressesRepo addressesRepo = new AddressesRepo(_context);
                    Address address = new Address()
                    {
                        streetAddress = jobCreateVM.Address,
                        city = jobCreateVM.City,
                        province = jobCreateVM.Province,
                        postalCode = ""
                    };
                    addressesRepo.CreateAddress(address);
                    addressId = address.addressID;
                }


                job = new Job()
                {
                    title = jobCreateVM.Title,
                    description = jobCreateVM.Description,
                    employerID = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    employeeID = null,
                    amount = jobCreateVM.Amount,
                    dateNeeded = jobCreateVM.dateNeeded,
                    dateExpired = jobCreateVM.dateExpired,
                    addressID = addressId

                };

                jobRepo.CreateJob(job);
                return RedirectToAction(nameof(Index), "Profile", new { areas = "" });
            }

            ViewData["addressID"] = new SelectList(_context.Addresses, "addressID", "addressID", job.addressID);
            ViewData["employerID"] = new SelectList(_context.Users, "Id", "Id", job.employerID);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            JobRepo jobRepo = new JobRepo(_context);

            if (id == null)
            {
                return NotFound();
            }

            var job = jobRepo.GetJob(id.Value);

            if (job == null)
            {
                return NotFound();
            }

            ViewData["addressID"] = new SelectList(_context.Addresses, "addressID", "addressID", job.addressID);
            ViewData["employerID"] = new SelectList(_context.Users, "Id", "Id", job.employerID);

            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("jobID,title,description,employerID,employeeID,amount,dateNeeded,dateExpired,addressID")] Job job)
        {
            JobRepo jobRepo = new JobRepo(_context);

            if (id != job.jobID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {   
               jobRepo.UpdateJob(job);
                return RedirectToAction(nameof(Index), "Profile", new { areas = "" });

            } else {
                return NotFound();          
            }

            //ViewData["addressID"] = new SelectList(_context.Addresses, "addressID", "addressID", job.addressID);
            //ViewData["employerID"] = new SelectList(_context.Users, "Id", "Id", job.employerID);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            JobRepo jobRepo = new JobRepo(_context);

            if (id == null)
            {
                return NotFound();
            }

            var job = jobRepo.GetJob(id.Value);

            //var job = await _context.Jobs
            //    .Include(j => j.Address)
            //    .Include(j => j.ApplicationUser)
            //    .FirstOrDefaultAsync(m => m.jobID == id);

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            JobRepo jobRepo = new JobRepo(_context);
            jobRepo.DeleteJob(id);

            return RedirectToAction(nameof(Index), "Profile", new { areas = "" });
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.jobID == id);
        }
    }
}
