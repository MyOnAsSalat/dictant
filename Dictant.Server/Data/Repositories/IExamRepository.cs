using Dictant.Shared.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictant.Server.Data.Repositories
{
    public interface IExamRepository
    {
        Task<Exam> Get(int id);
        void Create(Exam exam,string userName);
        void Delete(int id);
    }
}
