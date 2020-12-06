using System.Collections.Generic;
using System.Threading.Tasks;
using Dictant.Server.Data.Repositories;
using Dictant.Shared.Models.Blog;
using Microsoft.AspNetCore.Mvc;


namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IBlogRepository repository;
        public BlogController(IBlogRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("Get")][HttpGet]
        public IEnumerable<BlogPost> Get()
        {
            return repository.Get();
        }

        [HttpGet("{id}")]
        public async Task<BlogPost> Get(int id)
        {
            return await repository.Get(id);
        }

        [HttpPost("Post")]
        public void Post([FromBody] BlogPost value)
        {
            repository.Create(value);
        }

        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
