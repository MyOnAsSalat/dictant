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
        public int property = 1;
        private IEnumerable<DictantSource> Dictants;
        private string Text;
        private async Task ChangeAsync(string text, int property)
        {
            text = text.ToLower();
            Text = text;
            Dictants = (await http.GetJsonAsync<IEnumerable<DictantSource>>("https://localhost:5001/api/Dictant/get")).Where(x => x.Approved && ((text == "" || text == null) ? true : property == 1 ? x.Title.ToLower().Contains(text) : x.Description.ToLower().Contains(text)));
            OnSearch.InvokeAsync(Dictants);
        }
        private async Task ChangeType()
        {
            await ChangeAsync(Text, property);
        }
        [Parameter]
        public EventCallback<IEnumerable<DictantSource>> OnSearch { get; set; }
    }
}
