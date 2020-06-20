using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Server.Data;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        // GET: api/<ExamController>
        private TasksDbContext db;
        public ExamController(TasksDbContext db)
        {
            this.db = db;
        }

        // GET api/<ExamController>/5
        [HttpGet("{id}")]
        public Exam Get(int id)
        {
            return db.Exams.FirstOrDefault(x=>x.Id==id);
        }

        // POST api/<ExamController>
        [HttpPost]
        public void Post()
        {
        }

        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
