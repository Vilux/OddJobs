﻿using System;
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
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("jobID,title,description,employerID,employeeID,amount,dateNeeded,dateExpired,addressID")] Job job)
        {
            JobRepo jobRepo = new JobRepo(_context);

            if (ModelState.IsValid)
            {
                jobRepo.CreateJob(job);
                return RedirectToAction(nameof(Index));
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
               return RedirectToAction(nameof(Index));

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
           
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.jobID == id);
        }
    }
}
