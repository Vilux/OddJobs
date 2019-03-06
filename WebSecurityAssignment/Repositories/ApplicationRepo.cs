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
                    ApplicantID = employee.Id,
                    comments    = item.Comment
                });
            }
            return applicationList;
        }

        public ApplicationVM GetApplication(string applicantID, int jobID)
        {
            var application = _context.Applications.Where(a => a.ApplicantID == applicantID && a.JobID == jobID).FirstOrDefault();

            var employee = _context.Users.Find(applicantID);
            var job = _context.Jobs.Find(jobID);
            var employer = _context.Users.Find(job.employerID);

            if (application != null)
            {
                return new ApplicationVM()
                {
                    EmployerFN = employer.FirstName,
                    EmployerLN = employer.LastName,
                    EmployeeFN = employee.FirstName,
                    EmployeeLN = employee.LastName,
                    JobTitle = job.title,
                    ApplicantID = employee.Id,
                    JobID = job.jobID,
                    comments = application.Comment,
                };
            }
            return null;
        }

        public bool DeleteApplication(string applicantID, int jobID)
        {
            var application = _context.Applications.Where(a => a.ApplicantID == applicantID && a.JobID == jobID).FirstOrDefault();

            _context.Applications.Remove(application);
            _context.SaveChanges();
            return true;
        }


        public bool UpdateApplication(Application application)
        {
            _context.Update(application);
            _context.SaveChanges();
            return true;
        }
    }
}
