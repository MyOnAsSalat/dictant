using Dictant.Shared.Models.Tasks.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Dictant.Shared.Models.Tasks
{
    public static class DictantMock
    {
        public static DictantSource GetMock()
        {
            var source = new DictantSource();
            source.Title = "Veteran monologue";
            source.Description = "Описание диктанта";
            source.Approved = true;
            source.ApproverId = "Approver";
            source.OwnerId = "Creator";
            source.Text = GetWords();
            source.Timings = GetTimings();
            return source;
        }
        //","
        public static string GetWords()
        {
            var source = new TextSource();
            source.Words = new List<List<string>>();
            source.Words.Add(
                new List<string>()
                {
                    "brothers","of","the","mine","rejoice","swing","swing","swing","with","me"
                });
            source.Words.Add(
                new List<string>()
                {
                    "raise","your","pick","and","raise","your","voice","sing","sing","sing","with","me"
                });
            source.Words.Add(
                new List<string>()
                {
                    "down","and","down","into","the","deep","who","knows","what","we","will","find","beneath"
                });
            return JsonConvert.SerializeObject(source);
        }
        public static string GetTimings()
        {
            var source = new Timings();
            source.Segments = new List<double[]>();
            source.Segments.Add(new []{32d,4.8d});
            source.Segments.Add(new []{36.5d,4d});
            source.Segments.Add(new []{40.5d,4.1d});
            return JsonConvert.SerializeObject(source);
        }
    }
}
