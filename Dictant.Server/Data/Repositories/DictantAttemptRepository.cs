using Dictant.Shared.Models.Tasks;
using Dictant.Shared.Models.Tasks.JsonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictant.Server.Data.Repositories
{
    public class DictantAttemptRepository : IDictantAttemptRepository
    {
        private readonly TasksDbContext db;
        public DictantAttemptRepository(TasksDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<Attempt> Get()
        {
            return db.Attempts;
        }
        public IEnumerable<UserAttempt> GetUserAttempt(string userName)
        {
            return db.Attempts.Where(x => x.User == userName && x.Finished).Select(x => new UserAttempt() { Attempt = x, Title=x.Dictant.Title}) ;
        }
        public async Task<int> StartAttempt(Attempt newModel,string UserName)
        {
            newModel.User = UserName;
            newModel.Result = null;
            newModel.Start = DateTime.Now;
            newModel.Finished = false;
            newModel.Dictant = db.Dictants.FirstOrDefault(x => x.Id == newModel.Dictant.Id);
            var model = db.Attempts.Add(newModel);
            await db.SaveChangesAsync();
            return model.Entity.Id;
        }
        public async Task<ElapsedTime> FinishAttempt(AttemptResult newModel,string userName)
        {
            var model = await db.Attempts.FirstOrDefaultAsync(x=>x.Id == newModel.Id);
            if (model == null || model.User != userName || model.Finished) return null;
            model.Result = newModel.Result;
            model.Finished = true;
            model.End = DateTime.Now;
            db.Attempts.Update(model);
            await db.SaveChangesAsync();
            var elapsedTime = model.End - model.Start;
            return new ElapsedTime() { elapsedTime = string.Format("{0}:{1}", (int)elapsedTime.TotalMinutes, elapsedTime.Seconds) };
        }
        public async void Delete(int id)
        {
            db.Dictants.Remove(db.Dictants.FirstOrDefault(x=>x.Id == id));
            await db.SaveChangesAsync();
        }
    }
}
