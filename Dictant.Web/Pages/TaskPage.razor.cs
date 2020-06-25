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

namespace Dictant.Web.Pages
{
    public partial class TaskPage
    {
        public double CurrentPosition; 
        public DictantSource Source;
        public List<string> GetUniqueWords()
        {
            if(Text == null) return new List<string>(){ "null"};
            return Text.Words.SelectMany(x=>x).Distinct().ToList();
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
            Source = DictantMock.GetMock();
            //await Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        await Task.Delay(300);

            //    }
            //});
        }

        
        private async void Start()
        {
            var segments = GetTimings();
            if (!IsStarted)
            {
                IsStarted = true;
                interactButtonText = "Repeat";
                await js.AudioPlaySegment("zvuk",segments[LineIndex][0],segments[LineIndex][1]);
            }
            else
            {
                 js.AudioPlaySegment("zvuk",segments[LineIndex][0],segments[LineIndex][1]);
                 Repeats++;
            }
        }
        private async void Resume()
        {
          CurrentPosition = (await js.AudioGetCurrentTime("zvuk")-10d)*100d/(30d);
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
                        IsFinished = true;
                        //Finish
                        return;
                    }
                    LineIndex++;
                    WordInex = 0;
                    js.AudioPlaySegment("zvuk",GetTimings()[LineIndex][0],GetTimings()[LineIndex][1]);
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
