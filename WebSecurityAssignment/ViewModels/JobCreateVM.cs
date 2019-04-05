using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSecurityAssignment.ViewModels
{
    public class JobCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string EmployerID { get; set; }
        public float Amount { get; set; }
        public DateTime dateNeeded { get; set; }
        public string hoursNeeded { get; set; }
        public string minutesNeeded { get; set; }
        public DateTime dateExpired { get; set; }
        public string hoursExpired { get; set; }
        public string minutesExpired { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}
