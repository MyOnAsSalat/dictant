using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dictant.Shared.Models.Tasks;
namespace Dictant.Server.Data
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<DictantSource> Dictants { get;set; }
        public DbSet<Attempt> Attempts { get;set; }
    }
}
