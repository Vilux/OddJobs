﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Index(string id)
        {
            var listings = _context.Jobs.Where(j => j.employerID == id);
            return View(listings.ToList());
        }
    }
}