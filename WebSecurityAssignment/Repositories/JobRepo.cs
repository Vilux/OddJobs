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
    }
}
