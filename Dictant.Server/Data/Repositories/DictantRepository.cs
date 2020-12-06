using Dictant.Shared.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictant.Server.Data.Repositories
{
    public class DictantRepository : IDictantRepository
    {
        private readonly TasksDbContext db;
        public DictantRepository(TasksDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<DictantSource> Get()
        {
            return db.Dictants;
        }
        public async Task<int> GetCount()
        {
            return await db.Dictants.CountAsync();
        }
        public async void Approve(int id)
        {
            var model = await db.Dictants.FirstOrDefaultAsync(x => x.Id==id);
            model.Approved = true;
            db.Dictants.Update(model);
            await db.SaveChangesAsync();
        }
        public async void Reject(int id)
        {
            db.Dictants.Remove(db.Dictants.FirstOrDefault(x => x.Id==id));
            await db.SaveChangesAsync();
        }
        public async Task<DictantSource> Get(int id)
        {
            return await db.Dictants.FindAsync(id);
        }
        public async Task Create(DictantSource newModel,string userName)
        {
            var model = db.Dictants.FirstOrDefault(x => x.Id == newModel.Id);
            if (model != null)
            {
                if (model.OwnerId != userName) return;
                model.Title = newModel.Title;
                model.Text = newModel.Text;
                model.AudioSource = newModel.AudioSource;
                model.Timings = newModel.Timings;
                model.Description = newModel.Description;
                model.Public = newModel.Public;
                model.Approved = false;
                db.Dictants.Update(model);
                await db.SaveChangesAsync();
            }
            else
            {
                newModel.OwnerId = userName;
                newModel.Approved = false;
                db.Dictants.Add(newModel);
                await db.SaveChangesAsync();
            }
            
        }   
        public async void Delete(int id)
        {
            db.Dictants.Remove(db.Dictants.FirstOrDefault(x=>x.Id == id));
            await db.SaveChangesAsync();
        }
    }
}
