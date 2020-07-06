using Dictant.Shared.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Components.Problemset
{
    public partial class SearchComponent
    {
        public int property;
        private IEnumerable<DictantSource> Dictants;
        private async Task ChangeAsync(string text, int property)
        {
            this.Dictants = (await http.GetJsonAsync<IEnumerable<DictantSource>>("https://localhost:5001/api/Dictant/get")).Where(x => x.Approved);
            foreach (var Dictant in Dictants)
            {
                
            }
        }
    }
}
