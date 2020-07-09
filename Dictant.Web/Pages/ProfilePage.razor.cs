using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Shared.Models.Tasks;
using Dictant.Shared.Models.Tasks.JsonModels;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Dictant.Web.Pages
{
    public partial class ProfilePage
    {
        public IEnumerable<DictantSource> Tasks;
        public IEnumerable<UserAttempt> Attempts;
        protected override async Task OnInitializedAsync()
        {
            var userName = (await authStateProvider.GetAuthenticationStateAsync()).User.Identity.Name;
            Tasks = (await http.GetJsonAsync<IEnumerable<DictantSource>>("https://localhost:5001/api/Dictant/get")).Where(x => x.OwnerId == userName);
            Attempts = (await http.GetJsonAsync<IEnumerable<UserAttempt>>("https://localhost:5001/api/DictantAttempt/getuserattempt/"+userName));
        }
        private int GetMistakes(Attempt attempt)
        {
            return JsonConvert.DeserializeObject<ResultSource>(attempt.Result).MistakesCount;
        }
        private int GetRepeats(Attempt attempt)
        {
            return JsonConvert.DeserializeObject<ResultSource>(attempt.Result).RepeatsCount;
        }
        private string GetElapsedTime(Attempt attempt)
        {
            var elapsedTime = attempt.End - attempt.Start;
            return string.Format("{0}:{1}", (int)elapsedTime.TotalMinutes, elapsedTime.Seconds);
        }
    }
}
