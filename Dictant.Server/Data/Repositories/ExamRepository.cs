using Dictant.Shared.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Dictant.Server.Data.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly TasksDbContext db;
        public ExamRepository(TasksDbContext db)
        {
            this.db = db;
        }
        public async Task<Exam> Get(int id)
        {
            return await db.Exams.FirstOrDefaultAsync(x=>x.Id==id);
        }
        public void Create(Exam exam,string userName)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
