using Dictant.Shared.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictant.Server.Data.Repositories
{
    public interface IBlogRepository
    {
        IEnumerable<BlogPost> Get();
        Task<BlogPost> Get(int id);
        void Create(BlogPost value);
        void Delete(int id);

    }
}
