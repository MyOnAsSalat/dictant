using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Server.Data;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictantSourceController : ControllerBase
    {
        private TasksDbContext db;
        public DictantSourceController(TasksDbContext db)
        {
            this.db = db;
        }
        // GET api/<DictantSourceController>/5
        [HttpGet("{id}")]
        public async Task<DictantSource> Get(int id)
        {
            return await db.Dictants.FindAsync(id);
        }

        // POST api/<DictantSourceController>
        [HttpPost][Authorize]
        public void StartAttempt([FromBody] Attempt dto)
        {
            dto.User = HttpContext.User.Identity.Name;
            dto.Result = null;
            dto.Start = DateTime.Now;
            dto.Finished = false;
            db.Attempts.Add(dto);
            db.SaveChangesAsync();
        }
        [HttpPost][Authorize]
        public void FinishAttempt([FromBody] AttemptResult dto)
        {
            var model = db.Attempts.FirstOrDefault(x=>x.Id == dto.Id);
            if (model == null || model.User != HttpContext.User.Identity.Name || model.Finished) return;
            model.Result = dto.Result;
            model.Finished = true;
            model.End = DateTime.Now;
            db.Attempts.Add(model);
            db.SaveChangesAsync();
        }

        // DELETE api/<DictantSourceController>/5
        [HttpDelete("{id}")][Authorize]
        public async void Delete(int id)
        {
            db.Dictants.Remove(db.Dictants.FirstOrDefault(x=>x.Id == id));
            await db.SaveChangesAsync();
        }
    }
}
