using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Pages
{
    public partial class ProfilePage
    {
        public IEnumerable<DictantSource> Tasks;
        public IEnumerable<DictantSource> Dictants;
        protected override async Task OnInitializedAsync()
        {
            var userName = (await authStateProvider.GetAuthenticationStateAsync()).User.Identity.Name;
            Tasks = (await http.GetJsonAsync<IEnumerable<DictantSource>>("https://localhost:5001/api/Dictant/get")).Where(x => x.OwnerId == userName);

            Dictants = (await http.GetJsonAsync<IEnumerable<DictantSource>>("https://localhost:5001/api/Dictant/get")).Where(x => x.OwnerId == userName);
        }
    }
}
