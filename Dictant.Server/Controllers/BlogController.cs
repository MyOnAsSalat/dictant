using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dictant.Server.Data;
using Dictant.Shared.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private BlogDbContext db;
        public BlogController(BlogDbContext db)
        {
            this.db = db;
        }
        // GET: api/<BlogController>
        [HttpGet("Get")][HttpGet]
        public IEnumerable<BlogPost> Get()
        {
            return db.BlogPosts;
        }

        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public BlogPost Get(int id)
        {
            return db.BlogPosts.FirstOrDefault(x=>x.Id == id);
        }

        // POST api/<BlogController>
        [HttpPost("Post")]
        public void Post([FromBody] BlogPost value)
        {
            db.BlogPosts.Add(value); 
            db.SaveChanges();
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var model = db.BlogPosts.FirstOrDefault(x => x.Id == id);
            if (model == null) return;
            db.BlogPosts.Remove(model);
            await db.SaveChangesAsync();
        }
    }
}
