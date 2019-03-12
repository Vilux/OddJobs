using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSecurityAssignment.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSecurityAssignment.APIs.JobFeedAPI
{
    [Route("api/[controller]")]
    public class JobFeedAPIController : Controller
    {
 
        private readonly ApplicationDbContext Dbcontext;

        public JobFeedAPIController(ApplicationDbContext context) {
        Dbcontext = context;
        }


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Job> Get()
        {
            //return new string[] { "value1", "value2" };
            return Dbcontext.Jobs.ToList();
        }

        
        [HttpGet("{id}", Name = "GetJobs")]
        public IActionResult GetById(int id) {
            var item = Dbcontext.Jobs.FirstOrDefault(t => t.jobID == id);
            if (item == null) {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        /*
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
