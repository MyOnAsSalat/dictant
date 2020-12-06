using System.Collections.Generic;
using System.Threading.Tasks;
using Dictant.Server.Data.Repositories;
using Dictant.Server.Helpers;
using Dictant.Shared.Models.Tasks;
using Dictant.Shared.Models.Tasks.JsonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictantAttemptController : ControllerBase
    {
        private IDictantAttemptRepository repository;
        public DictantAttemptController(IDictantAttemptRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetUserAttempt/{userId}")]
        public IEnumerable<UserAttempt> GetUserAttempt(string userId)
        {
            return repository.GetUserAttempt(userId);
        }

        [HttpGet("Get")]
        public IEnumerable<Attempt> Get()
        {
            return repository.Get();
        }

        [HttpPost("StartAttempt")]
        public async Task<int> StartAttempt([FromBody] Attempt dto)
        {
           return await repository.StartAttempt(dto, UserHelper.GetName(HttpContext));
        }
        [HttpPost("FinishAttempt")]
        public async Task<ElapsedTime> FinishAttempt([FromBody] AttemptResult dto)
        {
           return await repository.FinishAttempt(dto,UserHelper.GetName(HttpContext));
        }

        [HttpDelete("{id}")][Authorize]
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
