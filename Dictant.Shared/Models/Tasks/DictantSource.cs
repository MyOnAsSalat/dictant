using System.ComponentModel.DataAnnotations;

namespace Dictant.Shared.Models.Tasks
{
    //
    public class DictantSource
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        //Отрезки времени
        public string Timings { get; set; }
        public string AudioSource { get; set; }
        public string OwnerId {get;set;}
        public string ApproverId {get;set;}
        public bool Approved { get;set;}
        public bool Public { get;set;}
    }
}