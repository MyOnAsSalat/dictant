using Dictant.Shared.Models.Blog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictant.Server.Data.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext db;
        public BlogRepository(BlogDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<BlogPost> Get()
        {
            return db.BlogPosts;
        }
        public async Task<BlogPost> Get(int id)
        {
            return await db.BlogPosts.FirstOrDefaultAsync(x=>x.Id == id);
        }
        public async void Create(BlogPost value)
        {
            db.BlogPosts.Add(value); 
            await db.SaveChangesAsync();
        }
        public async void Delete(int id)
        {
            var model = db.BlogPosts.FirstOrDefault(x => x.Id == id);
            if (model == null) return;
            db.BlogPosts.Remove(model);
            await db.SaveChangesAsync();
        }
    }
}
