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

        public JobFeedAPIController(ApplicationDbContext context)
        {
            Dbcontext = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<JobVM> GetAll()
        {
            JobRepo jobRepo = new JobRepo(Dbcontext);
            return jobRepo.GetAllJobs();
        }

        /*
        [HttpGet("{id}", Name = "GetJobs")]
        public IActionResult GetById(int id) {
            var item = Dbcontext.Jobs.FirstOrDefault(t => t.jobID == id);
            if (item == null) {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        */

       
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
