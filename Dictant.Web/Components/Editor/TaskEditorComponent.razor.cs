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
        private int LineCount = 0;
        protected override async Task OnInitializedAsync()
        {
            Source = new DictantSource();
            var audioSource = new AudioSource();
            audioSource.Type = "audio";
            Source.AudioSource = JsonConvert.SerializeObject(audioSource);
            var timings = new Timings();
            timings.Segments = new List<double[]>();
            Source.Timings = JsonConvert.SerializeObject(timings);
            var textSource = new TextSource();
            textSource.Words = new List<List<string>>();
            Source.Text = JsonConvert.SerializeObject(textSource);
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

        private List<double[]> Timings
        {
            get => JsonConvert.DeserializeObject<Timings>(Source.Timings).Segments;
            set
            {
                var model = JsonConvert.DeserializeObject<Timings>(Source.Timings);
                model.Segments = value;
                Source.Timings = JsonConvert.SerializeObject(model);
            }
        }

        private List<List<string>> Words
        {
            get => JsonConvert.DeserializeObject<TextSource>(Source.Text).Words;
            set
            {
                var model = JsonConvert.DeserializeObject<TextSource>(Source.Timings);
                model.Words = value;
                Source.Text = JsonConvert.SerializeObject(model);
            }
        }

        private void UpdateWords(int i, string args)
        {
            var words = Words;
            words[i] = args.Split("",StringSplitOptions.RemoveEmptyEntries).ToList();
            Words = words;
        }
        private void UpdateTimings(int i, bool isStart , string args)
        {
            var timings = Timings;
            timings[i][isStart ? 0 : 1] = int.Parse(args);
            Timings = timings;
        }

        private void NewLine()
        {
            var timings = Timings;
            timings.Add(new[] {0d, 0d});
            Timings = timings;
            var words = Words;
            words.Add(new List<string>());
            Words = words;
            LineCount++;
        }

        private void RemoveLine()
        {
            Timings = Timings.Take(Timings.Count-1).ToList();
            Words = Words.Take(Words.Count-1).ToList();;
            LineCount--;
        }
    }
}
