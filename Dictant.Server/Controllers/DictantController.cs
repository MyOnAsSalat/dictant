using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Server.Data.Repositories;
using Dictant.Server.Helpers;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;




namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictantController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IDictantRepository repository;
        public DictantController(IDictantRepository repository, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            this.repository = repository;
        }
        
        [HttpGet("Get")]
        public IEnumerable<DictantSource> Get()
        {
            return repository.Get();
        }

        
        [HttpGet("GetCount")]
        public async Task<int> GetCount()
        {
            return await repository.GetCount();
        }
        [HttpGet("Approve/{id}")][Authorize]
        public async void Approve(int id)
        {
            if (HttpContext.User.Claims.All(x => x.Type != "moderator")) return;
            repository.Approve(id);
        }

        [HttpGet("Reject/{id}")][Authorize]
        public void Reject(int id)
        {
            if (HttpContext.User.Claims.All(x => x.Type != "moderator")) return;
            repository.Reject(id);
        }


        [HttpGet("Get/{id}")]
        public async Task<DictantSource> Get(int id)
        {
            return await repository.Get(id);
        }

        
        [HttpPost("Post")][Authorize]
        public async void PostAsync([FromBody] DictantSource dto)
        {
           await repository.Create(dto,UserHelper.GetName(HttpContext));          
        }        


        [HttpDelete("{id}")][Authorize]
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
