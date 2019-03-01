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
            var jobs = _context.Jobs ;

            //var jobsAddresses =
            //    from j in _context.Jobs
            //    from a in _context.Addresses
            //    where j.addressID == a.addressID
            //    select new
            //    {
            //        j.jobID,
            //        j.title,
            //        j.description,
            //        j.employeeID,
            //        j.employerID,
            //        j.amount,
            //        j.dateNeeded,
            //        j.dateExpired,
            //        j.addressID,
            //        a.streetAddress,
            //        a.city,
            //        a.province,
            //        a.postalCode
            //    };

            //var jobsAddressesUsers =
            //    from ja in jobsAddresses
            //    from c in _context.Users
            //    where ja.employeeID

            List <Job> jobList = new List<Job>();
           
            foreach (var item in jobs)
            {
                var completeAddress = _context.Addresses.Find(item.addressID);

                jobList.Add(new Job() {             
                    jobID = item.jobID,
                    title = item.title,
                    description = item.description,
                    employeeID = item.employeeID,
                    employerID = item.employerID,
                    amount = item.amount,
                    dateNeeded = item.dateNeeded,
                    dateExpired = item.dateExpired,
                    addressID = item.addressID,
                    //address = (completeAddress.streetAddress + " " + completeAddress.city + " " + completeAddress.province + " " + completeAddress.postalCode)
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
