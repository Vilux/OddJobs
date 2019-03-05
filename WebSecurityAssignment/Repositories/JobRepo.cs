using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.ViewModels;

namespace WebSecurityAssignment.Repositories
{
    public class JobRepo
    {
        ApplicationDbContext _context;

        public JobRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<JobVM> GetAllJobs()
        
        {
            var jobs = _context.Jobs ;

            List<JobVM> jobList = new List<JobVM>();

            foreach (var item in jobs)
            {
                var completeAddress = _context.Addresses.Find(item.addressID);
                var employee = _context.Users.Find(item.employeeID);
                var employer = _context.Users.Find(item.employerID);

                //will implement try catch - AJ

                if(employee==null)
                {
                    employee = new ApplicationUser();

                }

                if (employer == null)
                {
                    employee = new ApplicationUser();

                }

                jobList.Add(new JobVM()
                {
                    ID = item.jobID,
                    Title = item.title,
                    Description = item.description,
                    EmployeeName = (employee.FirstName + " " + employee.LastName),
                    EmployerName = (employer.FirstName + " " + employer.LastName),
                    Amount = item.amount,
                    dateNeeded = item.dateNeeded,
                    dateExpired = item.dateExpired,
                    Address = (completeAddress.streetAddress + " " + completeAddress.city + " " + completeAddress.province + " " + completeAddress.postalCode)
                });
            }

            return jobList;
        }

        //Stephen: added this for the listings page on users profile
        public List<JobVM> GetAllJobsByEmployer(string id)

        {
            var jobs = _context.Jobs.Where(j => j.employerID == id);

            List<JobVM> jobList = new List<JobVM>();

            foreach (var item in jobs)
            {
                var completeAddress = _context.Addresses.Find(item.addressID);
                var employee = _context.Users.Find(item.employeeID);
                var employer = _context.Users.Find(item.employerID);

                //will implement try catch - AJ

                if (employee == null)
                {
                    employee = new ApplicationUser();

                }

                if (employer == null)
                {
                    employee = new ApplicationUser();

                }

                jobList.Add(new JobVM()
                {
                    ID = item.jobID,
                    Title = item.title,
                    Description = item.description,
                    EmployeeName = (employee.FirstName + " " + employee.LastName),
                    EmployerName = (employer.FirstName + " " + employer.LastName),
                    Amount = item.amount,
                    dateNeeded = item.dateNeeded,
                    dateExpired = item.dateExpired,
                    Address = (completeAddress.streetAddress + " " + completeAddress.city + " " + completeAddress.province + " " + completeAddress.postalCode)
                });
            }

            return jobList;
        }

        public Job GetJob(int jobID)
        {
            var job = _context.Jobs.Where(j => j.jobID == jobID).FirstOrDefault();
            if (job != null)
            {
                return new Job() {
                    jobID = job.jobID,
                    title = job.title,
                    description = job.description,
                    employeeID = job.employeeID,
                    employerID = job.employerID,
                    amount = job.amount,
                    dateNeeded = job.dateNeeded,
                    dateExpired = job.dateExpired,
                    addressID = job.addressID };
            }
            return null;
        }

        public bool DeleteJob(int jobID)
        {
            var job = _context.Jobs.Where(j => j.jobID == jobID).FirstOrDefault();

            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateJob(Job job)
        {
            _context.Update(job);
            _context.SaveChanges();
            return true;
        }

        public bool CreateJob(Job job)
        {
            var jobs = _context.Jobs;
            jobs.Add(job);

            _context.SaveChanges();
            return true;
        }
    }
}
