using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dictant.Server.Data;
using Dictant.Server.Helpers;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictantController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private TasksDbContext db;
        public DictantController(TasksDbContext db, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            this.db = db;
        }
        
        [HttpGet("Get")]
        public IEnumerable<DictantSource> Get()
        {
            return db.Dictants;
        }

        
        [HttpGet("GetCount")]
        public int GetCount()
        {
            return db.Dictants.Count();
        }
        [HttpGet("Approve/{id}")]
        public void Approve(int id)
        {
            var model =  db.Dictants.FirstOrDefault(x => x.Id==id);
            model.Approved = true;
            db.Dictants.Update(model);
            db.SaveChanges();
        }

        [HttpGet("Reject/{id}")]
        public void Reject(int id)
        {
            //if (HttpContext.User.Claims.All(x => x.Type != "moderator")) return;
            db.Dictants.Remove(db.Dictants.FirstOrDefault(x => x.Id==id));
            db.SaveChanges();
        }


        [HttpGet("Get/{id}")]
        public async Task<DictantSource> Get(int id)
        {
            return await db.Dictants.FindAsync(id);
        }

        
        [HttpPost("Post")]
        public async Task PostAsync([FromBody] DictantSource dto)
        {
            var model = db.Dictants.FirstOrDefault(x => x.Id == dto.Id);
            if (model != null)
            {
                if (model.OwnerId != HttpContext.User.Identity.Name) return;
                model.Title = dto.Title;
                model.Text = dto.Text;
                model.AudioSource = dto.AudioSource;
                model.Timings = dto.Timings;
                model.Description = dto.Description;
                model.Public = dto.Public;
                model.Approved = false;
                db.Dictants.Update(model);
                db.SaveChanges();
            }
            else
            {
                dto.OwnerId = UserHelper.GetName(HttpContext);
                dto.Approved = false;
                db.Dictants.Add(dto);
                db.SaveChanges();
            }
            
        }        


        [HttpDelete("{id}")][Authorize]
        public async void Delete(int id)
        {
            db.Dictants.Remove(db.Dictants.FirstOrDefault(x=>x.Id == id));
            await db.SaveChangesAsync();
        }
    }
}
