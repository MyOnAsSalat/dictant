using Dictant.Shared.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dictant.Shared.Models.Tasks.JsonModels;

namespace Dictant.Web.Components.Editor
{
    public partial class TaskEditorComponent
    {
        private DictantSource Source;
        protected override async Task OnInitializedAsync()
        {
            Source = new DictantSource();
            var audioSource = new AudioSource();
            audioSource.Type = "audio";
            Source.AudioSource = JsonConvert.SerializeObject(audioSource);
        }

        private string AudioSource
        {
            get => JsonConvert.DeserializeObject<AudioSource>(Source.AudioSource).URL;
            set
            {
                var model = JsonConvert.DeserializeObject<AudioSource>(Source.AudioSource);
                model.URL = value;
                Source.AudioSource = JsonConvert.SerializeObject(model);
            }
        }
    }
}
