using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSecurityAssignment.ViewModels
{
    public class ApplicationVM
    {
        public int ID { get; set; }
        public string EmployerFN { get; set; }
        public string EmployerLN { get; set; }
        public string EmployeeFN { get; set; }
        public string EmployeeLN { get; set; }
        public string JobTitle { get; set; }
        public int    JobID { get; set; }
        public string comments { get; set; }
    }
}
