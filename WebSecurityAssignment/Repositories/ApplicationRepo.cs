using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.ViewModels;

namespace WebSecurityAssignment.Repositories
{
    public class ApplicationRepo
    {
        ApplicationDbContext _context;

        public ApplicationRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<ApplicationVM> GetAllApplications()
        {
            var applications = _context.Applications;

            List<ApplicationVM> applicationList = new List<ApplicationVM>();

            foreach (var item in applications)
            {
                var employee = _context.Users.Find(item.ApplicantID);
                var job = _context.Jobs.Find(item.JobID);
                var employer = _context.Users.Find(job.employerID);

                applicationList.Add(new ApplicationVM()
                {
                    EmployeeFN  = employee.FirstName,
                    EmployeeLN  = employee.LastName,
                    EmployerFN  = employer.FirstName,
                    EmployerLN  = employer.LastName,
                    JobTitle    = job.title,
                    JobID       = job.jobID,
                    comments    = item.Comment
                });
            }
            return applicationList;
        }
    }
}
