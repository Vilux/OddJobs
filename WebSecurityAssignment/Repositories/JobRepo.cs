using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSecurityAssignment.Data;

namespace WebSecurityAssignment.Repositories
{
    public class JobRepo
    {
        ApplicationDbContext _context;

        public JobRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<Job> GetAllJobs()
        {
            var jobs = _context.Jobs;

            List<Job> jobList = new List<Job>();

            foreach (var item in jobs)
            {
                jobList.Add(new Job() { title = item.title,
                    description = item.description,
                    employeeID = item.employeeID,
                    employerID = item.employerID,
                    amount = item.amount,
                    dateNeeded = item.dateNeeded,
                    dateExpired = item.dateExpired,
                    addressID = item.addressID
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

        public bool UpdateJob(int jobID, string title, 
            string description, string employerID, string employeeID,
            float amount, DateTime dateNeeded, DateTime dateExpired, int addressID)
        {
            var job = _context.Jobs.Where(j => j.jobID == jobID).FirstOrDefault();
            // Remember you can't update the primary key without 
            // causing trouble.  Just update the review and score
            // for now.

            job.title = title;
            job.description = description;
            job.employerID = employerID;
            job.employeeID = employeeID;
            job.amount = amount;
            job.dateNeeded = dateNeeded;
            job.dateExpired = dateExpired;
            job.addressID = addressID;

            _context.SaveChanges();
            return true;
        }
    }
}
