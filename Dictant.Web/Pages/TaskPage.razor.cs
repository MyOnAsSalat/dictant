using Dictant.Shared.Models.Tasks;
using Dictant.Shared.Models.Tasks.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Pages
{
    public partial class TaskPage
    {
        public DictantSource Source;
        public List<string> GetUniqueWords()
        {
            if(Source == null) return new List<string>(){ "null"};
            return JsonConvert.DeserializeObject<TextSource>(Source.Text).Words.SelectMany(x=>x).Distinct().ToList();

        }
        public override Task SetParametersAsync(ParameterView parameters)
        {
            Source = DictantMock.GetMock();
            return base.SetParametersAsync(parameters);
        }
    }
}
