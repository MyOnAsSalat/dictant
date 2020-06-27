﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dictant.Server.Data;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictantController : ControllerBase
    {
        private TasksDbContext db;
        public DictantController(TasksDbContext db)
        {
            this.db = db;
        }
        
        [HttpGet]
        public IEnumerable<DictantSource> Get()
        {
            return db.Dictants;
        }

        
        [HttpGet("{id}")]
        public int GetCount(int id)
        {
            return db.Dictants.Count();
        }

        public void Approve(int id)
        {
            var model =  db.Dictants.FirstOrDefault(x => x.Id==id);
            model.Approved = true;
            db.Dictants.Update(model);
            db.SaveChanges();
        }

        [HttpPost("{id}")]
        public void Reject(int id)
        {
            if (HttpContext.User.Claims.All(x => x.Type != "moderator")) return;
            db.Dictants.Remove(db.Dictants.FirstOrDefault(x => x.Id==id));
            db.SaveChanges();
        }


        [HttpGet("{id}")]
        public async Task<DictantSource> Get(int id)
        {
            return await db.Dictants.FindAsync(id);
        }

        
        [HttpPost][Authorize]
        public void Post([FromBody] DictantSource dto)
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
                //Временно
                model.Approved = true;
                //
                db.Dictants.Update(model);
                db.SaveChanges();
            }
            else
            {
                dto.OwnerId = HttpContext.User.Identity.Name;
                //Временно
                dto.Approved = true;
                db.Dictants.Add(dto);
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