using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Shared.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace Dictant.Server.Data
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        public DbSet<BlogPost> BlogPosts { get;set; }
    }
}
