using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebSecurityAssignment.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactInfo { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; }
    }

}
