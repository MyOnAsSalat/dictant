using Dictant.Shared.Models.Tasks;
using Dictant.Shared.Models.Tasks.JsonModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using Dictant.Web.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace Dictant.Web.Pages
{
    public partial class TaskPage
    {
        public DictantSource Source;
        private List<string> _uniqueWords;
        private DateTime startTime;
        private TimeSpan elapsedTime;
        public List<string> GetUniqueWords()
        {
            if(Text == null) return new List<string>(){ "null"};
            if (_uniqueWords == null)
            {
                Random rnd = new Random();
                _uniqueWords = Text.Words.SelectMany(x => x).Distinct().OrderBy(x => rnd.Next()).ToList();
            }
            return _uniqueWords;
        }
        TextSource _text;

        private TextSource Text
        {
            get 
            {  
                if(Source == null) return null;
                if(_text == null) _text = JsonConvert.DeserializeObject<TextSource>(Source.Text);
                return _text;
            }
        }

        public List<double[]> GetTimings()
        {
            if (Source == null) return new List<double[]>();
            return JsonConvert.DeserializeObject<Timings>(Source.Timings).Segments;
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            return base.SetParametersAsync(parameters);
        }
        protected override async Task OnInitializedAsync()
        {
            //Source = DictantMock.GetMock();
            if (QueryHelpers.ParseQuery(navigationManager.ToAbsoluteUri(navigationManager.Uri).Query).TryGetValue("id", out var id))
            {
                Source = await http.GetJsonAsync<DictantSource>("https://localhost:5001/api/Dictant/get/" + id);
            }
        }

        
        private void Start()
        {
            var segments = GetTimings();
            if (!IsStarted)
            {
                startTime = DateTime.Now;
                IsStarted = true;
                interactButtonText = "Repeat";
                 js.AudioPlaySegment("zvuk",segments[LineIndex][0],segments[LineIndex][1]- segments[LineIndex][0] + 1);
            }
            else
            {
                 js.AudioPlaySegment("zvuk",segments[LineIndex][0],segments[LineIndex][1] - segments[LineIndex][0]);
                 Repeats++;
            }
        }


        private List<string> Line = new List<string>();
        private string wordbox = "";
        private int LineIndex = 0;
        private int WordInex = 0;
        private bool IsStarted = false;
        private bool IsFinished = false;
        private int Repeats = 0;
        private int MistakesCount = 0;
        private string interactButtonText = "start";
        private void InputKeyDown(KeyboardEventArgs e)
        {
            if (e.Code != "Space") return;
            var word = wordbox.Trim().ToLower();
            if (word == Text.Words[LineIndex][WordInex])
            {
                if (WordInex == 0)
                {
                    Line.Add("");
                }
                Line[LineIndex] = Line[LineIndex] + " " + word;
                if (WordInex == Text.Words[LineIndex].Count - 1)
                {
                    if (LineIndex == Text.Words.Count - 1)
                    {
                        elapsedTime = DateTime.Now - startTime;
                        IsFinished = true;
                        //Finish
                        return;
                    }
                    LineIndex++;
                    WordInex = 0;
                    js.AudioPlaySegment("zvuk",GetTimings()[LineIndex][0],GetTimings()[LineIndex][1]- GetTimings()[LineIndex][0]);
                }
                else
                {
                    WordInex++;
                }
            }
            else
            {
                MistakesCount++;
            }
            wordbox = "";
        }


        private async void Stop()
        {
            await js.AudioPlay("zvuk");
            //await AudioPlayer.PauseCurrentAudio();
            //Update();
        }
    }
}
