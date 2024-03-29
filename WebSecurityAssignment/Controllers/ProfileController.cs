﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Repositories;

namespace WebSecurityAssignment.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;
        private JobRepo jobRepo;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
            jobRepo = new JobRepo(_context);
        }

        //Listings view
        public IActionResult Index()
        {
            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            JobRepo jobRepo = new JobRepo(_context);

            var listings = jobRepo.GetAllJobsByEmployer(id);           

            return View(listings.ToList());
        }

        public IActionResult Jobs()
        {
            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            JobRepo jobRepo = new JobRepo(_context);

            var listings = jobRepo.GetAllJobsByEmployee(id);

            return View(listings.ToList());
        }

        public IActionResult Applications()
        {
            ApplicationRepo applicationRepo = new ApplicationRepo(_context);

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applications = applicationRepo.GetAllApplications(id);

            return View(applications.ToList());
        }
    }
}