using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.ViewModels;

namespace WebSecurityAssignment.Repositories
{
	public class UserRepo
	{
		ApplicationDbContext _context;

		public UserRepo(ApplicationDbContext context)
		{
			this._context = context;
		}

		// Get all users in the database.
		public IEnumerable<UserVM> All()
		{
			var users = _context.Users.Select(u => new UserVM()
			{
				Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,

            });
			return users;
		}

        public UserVM GetUser(string email)
        {
            var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                return new UserVM() { Email = user.Email };
            }
            return null;
        }

        public bool Remove(string email)
        {
            var user = _context.Users.Where(u => u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
