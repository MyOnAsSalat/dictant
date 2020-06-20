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
            source.Title = "Название диктанта";
            source.Description = "Описание диктанта";
            source.Approved = true;
            source.ApproverId = "Approver";
            source.OwnerId = "Creator";
            source.Text = GetWords();

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
                    "down","and","down","into","the","deep","who","knows","what","we’ll","find","beneath"
                });
            return JsonConvert.SerializeObject(source);
        }
    }
}
