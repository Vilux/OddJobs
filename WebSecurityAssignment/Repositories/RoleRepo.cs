using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.ViewModels;

namespace WebSecurityAssignment.Repositories
{
	public class RoleRepo
	{
		ApplicationDbContext _context;

		public RoleRepo(ApplicationDbContext context)
		{
			this._context = context;
		}

		public List<RoleVM> GetAllRoles()
		{
			var roles = _context.Roles;
			List<RoleVM> roleList = new List<RoleVM>();

          

			foreach (var item in roles)
			{
				roleList.Add(new RoleVM() { RoleName = item.Name, Id = item.Id });
			}
			return roleList;
		}

		public RoleVM GetRole(string roleName)
		{
			var role = _context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
			if (role != null)
			{
				return new RoleVM() { RoleName = role.Name, Id = role.Id };
			}
			return null;
		}

		public bool CreateRole(string roleName, string id)
		{
			var role = GetRole(roleName);
			if (role != null)
			{
				return false;
			}
			_context.Roles.Add(new IdentityRole
			{
				Name = roleName,
				Id = id,
				// Sqlite may behave better with ToUpper()
				NormalizedName = roleName.ToUpper()
			});
			_context.SaveChanges();
			return true;
		}

        public bool RemoveRole(string roleName, string id)
        {
            var role = _context.Roles.Where(r => r.NormalizedName.Equals(roleName.ToUpper(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }
    }
}

