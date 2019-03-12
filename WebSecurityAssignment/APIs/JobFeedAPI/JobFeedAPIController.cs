using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSecurityAssignment.Data;
using WebSecurityAssignment.Repositories;
using WebSecurityAssignment.ViewModels;

namespace WebSecurityAssignment.APIs.JobFeedAPI
{
    [Route("api/[controller]")]
    public class JobFeedAPIController : Controller
    {
        private readonly ApplicationDbContext Dbcontext;
        public JobFeedAPIController(ApplicationDbContext context) {
        Dbcontext = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<JobVM> Get()
        {
            JobRepo jobRepo = new JobRepo(Dbcontext);
            var jobs = jobRepo.GetAllJobs();
            return jobs.ToList();
        }

        /*
      [HttpGet]
      [Route("Get")]
      public IEnumerable<Job> Get()
      {
          return Dbcontext.Jobs.ToList();
      }
      */
    }
}
