﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Pages
{
    public partial class ModerationPage
    {
        public IEnumerable<DictantSource> Dictants;
        protected override async Task OnInitializedAsync()
        {
            Dictants = (await http.GetJsonAsync<IEnumerable<DictantSource>>("https://localhost:5001/api/Dictant/get")).Where(x=>!x.Approved);
        }
    }
}
