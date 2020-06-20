using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictant.Shared.Models.Tasks
{
    public class Attempt
    {
        [Key]
        public int Id { get; set; }
        [Required][ForeignKey("DictantSource")]
        public DictantSource Dictant {get;set; }
        [Required]
        public string User { get; set; }
        public string Type { get;set;}
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public bool Finished { get; set; }
        public string Result { get; set; }

    }
}
