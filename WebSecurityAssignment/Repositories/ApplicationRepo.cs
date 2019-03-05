using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSecurityAssignment.Data;

namespace WebSecurityAssignment.Repositories
{
    class ApplicationRepo
    {
        ApplicationDbContext _context;

        public ApplicationRepo (ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<Application> GetAllApplications()
        {
            var applications = _context.Applications;
            List<Application> applicationList = new List<Application>();

            foreach (var item in applications)
            {
                applicationList.Add(new Application()
                {
                    ApplicantID = item.ApplicantID,
                    JobID = item.JobID,
                    Comment = item.Comment
                });
            }
            return applicationList;
        }

        public Application GetApplication(string ApplicantID, int JobID)
        {
            var application = _context.Applications.Where(a => a.ApplicantID == ApplicantID && a.JobID == JobID).FirstOrDefault();
            if (application != null)
            {
                return new Application()
                {
                    ApplicantID = application.ApplicantID,
                    JobID = application.JobID,
                    Comment = application.Comment
                };
            }
            return null;
        }

        public bool RemoveApplication(string ApplicantID, int JobID)
        {
            var application = _context.Applications.Where(a => a.ApplicantID == ApplicantID && a.JobID == JobID).FirstOrDefault();

            _context.Applications.Remove(application);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateApplication(string ApplicantID, int JobID, string Comment)
        {
            var application = _context.Applications.Where(a => a.ApplicantID == ApplicantID && a.JobID == JobID).FirstOrDefault();
            // Remember you can't update the primary key without 
            // causing trouble.  Just update the review and score
            // for now.
            application.Comment = Comment;

            _context.SaveChanges();
            return true;
        }

        public bool CreateApplication(string ApplicantID, int JobID, string Comment)
        {
            var application = GetApplication(ApplicantID, JobID);
            if (application != null)
            {
                return false;
            }
            _context.Applications.Add(new Application
            {
                ApplicantID = ApplicantID,
                JobID = JobID,
                Comment = Comment
            });
            _context.SaveChanges();
            return true;
        }
    }
}
