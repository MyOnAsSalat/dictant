using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Server.Data;
using Dictant.Server.Data.Repositories;
using Dictant.Server.Helpers;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Dictant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private IExamRepository repository;
        public ExamController(IExamRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<Exam> Get(int id)
        {
            return await repository.Get(id);
        }


        [HttpPost][Authorize]
        public void Post([FromBody] Exam dto)
        {
            repository.Create(dto,UserHelper.GetName(HttpContext));
        }

        [HttpDelete("{id}")][Authorize]
        public void Delete(int id)
        {
        }
    }
}
