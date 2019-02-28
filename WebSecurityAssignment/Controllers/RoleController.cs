using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Repositories;
using WebSecurityAssignment.ViewModels;

namespace WebSecurityAssignment.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoleController : Controller
	{
		ApplicationDbContext _context;

		public RoleController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Role
		public ActionResult Index()
		{
			RoleRepo roleRepo = new RoleRepo(_context);
			return View(roleRepo.GetAllRoles());
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(RoleVM roleVM)
		{
			if (ModelState.IsValid)
			{
				RoleRepo roleRepo = new RoleRepo(_context);
				var success = roleRepo.CreateRole(roleVM.RoleName, roleVM.Id);
				if (success)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			ViewBag.Error = "An error occurred while creating this role. Please try again.";
			return View();
		}

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_context);
                var success = roleRepo.RemoveRole(roleVM.RoleName, roleVM.Id);
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