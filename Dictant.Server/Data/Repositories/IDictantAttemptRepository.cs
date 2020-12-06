using Dictant.Shared.Models.Tasks;
using Dictant.Shared.Models.Tasks.JsonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dictant.Server.Data.Repositories
{
    public interface IDictantAttemptRepository
    {
        IEnumerable<Attempt> Get();
        IEnumerable<UserAttempt> GetUserAttempt(string userId);
        Task<int> StartAttempt(Attempt newModel,string userName);
        Task<ElapsedTime> FinishAttempt(AttemptResult newModel,string userName);
        void Delete(int id);
    }
}
