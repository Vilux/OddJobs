using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSecurityAssignment.ViewModels
{
    public class JobVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public string EmployeeName { get; set; }
        public string EmployerName { get; set; }
        public string Address { get; set; }
        [Display(Name = "Date Needed")]
        public DateTime dateNeeded { get; set; }
        [Display(Name = "Date Expired")]
        public DateTime dateExpired { get; set; }

        public string employeeID { get; set; }
    }
}
