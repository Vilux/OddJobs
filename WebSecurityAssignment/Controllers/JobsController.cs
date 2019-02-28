using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Repositories;

namespace WebSecurityAssignment.Controllers
{
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
            ViewData["addressID"] = new SelectList(_context.Addresses, "addressID", "addressID");
            ViewData["employerID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("jobID,title,description,employerID,employeeID,amount,dateNeeded,dateExpired,addressID")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["addressID"] = new SelectList(_context.Addresses, "addressID", "addressID", job.addressID);
            ViewData["employerID"] = new SelectList(_context.Users, "Id", "Id", job.employerID);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["addressID"] = new SelectList(_context.Addresses, "addressID", "addressID", job.addressID);
            ViewData["employerID"] = new SelectList(_context.Users, "Id", "Id", job.employerID);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("jobID,title,description,employerID,employeeID,amount,dateNeeded,dateExpired,addressID")] Job job)
        {
            if (id != job.jobID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.jobID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["addressID"] = new SelectList(_context.Addresses, "addressID", "addressID", job.addressID);
            ViewData["employerID"] = new SelectList(_context.Users, "Id", "Id", job.employerID);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Address)
                .Include(j => j.ApplicationUser)
                .FirstOrDefaultAsync(m => m.jobID == id);
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
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.jobID == id);
        }
    }
}
