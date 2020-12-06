using Dictant.Shared.Models.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dictant.Server.Data.Repositories
{
    public interface IDictantRepository
    {
        IEnumerable<DictantSource> Get();
        Task<int> GetCount();
        void Approve(int id);
        void Reject(int id);
        Task<DictantSource> Get(int id);
        Task Create(DictantSource dto,string userName);
        void Delete(int id);

    }
}
