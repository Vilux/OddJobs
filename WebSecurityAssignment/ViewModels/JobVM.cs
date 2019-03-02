using System;
using System.Collections.Generic;
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
        public DateTime dateNeeded { get; set; }
        public DateTime dateExpired { get; set; }
    }
}
