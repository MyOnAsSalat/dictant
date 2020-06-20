using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dictant.Shared.Models.Tasks
{
    public class Exam
    {
        [Key]
        public int Id {get;set; }
        public string Title {get;set; }
        [Required]
        public DictantSource Dictant {get;set; }
        public string Type {get;set; }
        [Required]
        public string OwnerId {get;set; }
        [Required]
        public DateTimeOffset Start { get; set; }
        [Required]
        public DateTimeOffset End { get; set; }
        public string Participants {get;set; }
    }
}
