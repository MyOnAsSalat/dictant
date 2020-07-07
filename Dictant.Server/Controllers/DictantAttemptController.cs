using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Server.Data;
using Dictant.Server.Helpers;
using Dictant.Shared.Models.Tasks;
using Dictant.Shared.Models.Tasks.JsonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictantAttemptController : ControllerBase
    {
        private TasksDbContext db;
        public DictantAttemptController(TasksDbContext db)
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
        [HttpPost("StartAttempt")]
        public int StartAttempt([FromBody] Attempt dto)
        {
            dto.User = UserHelper.GetName(HttpContext);
            dto.Result = null;
            dto.Start = DateTime.Now;
            dto.Finished = false;
            dto.Dictant = db.Dictants.FirstOrDefault(x => x.Id == dto.Dictant.Id);
            var model = db.Attempts.Add(dto);
            db.SaveChanges();
            return model.Entity.Id;
        }
        [HttpPost("FinishAttempt")]
        public ElapsedTime FinishAttempt([FromBody] AttemptResult dto)
        {
            var model = db.Attempts.FirstOrDefault(x=>x.Id == dto.Id);
            if (model == null || model.User != UserHelper.GetName(HttpContext) || model.Finished) return null;
            model.Result = dto.Result;
            model.Finished = true;
            model.End = DateTime.Now;
            db.Attempts.Update(model);
            db.SaveChanges();
            var elapsedTime = model.End - model.Start;
            return new ElapsedTime() { elapsedTime = string.Format("{0}:{1}", (int)elapsedTime.TotalMinutes, elapsedTime.Seconds) };
        }

        // DELETE api/<DictantSourceController>/
        [HttpDelete("{id}")][Authorize]
        public async void Delete(int id)
        {
            db.Dictants.Remove(db.Dictants.FirstOrDefault(x=>x.Id == id));
            await db.SaveChangesAsync();
        }
    }
}
