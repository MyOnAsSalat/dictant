using Dictant.Shared.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Pages
{
    public partial class ProblemsetPage
    {
        public IEnumerable<DictantSource> Dictants;
        protected override async Task OnInitializedAsync()
        {
            Dictants = (await http.GetJsonAsync<IEnumerable<DictantSource>>("https://localhost:5001/api/Dictant/get")).Where(x=>x.Approved);
        }
    }
}
