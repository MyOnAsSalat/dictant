using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dictant.Shared.Models.Blog
{
    public class BlogPost
    {
        [Key]
        public int Id {get;set; }
        public string Title {get;set; }
        public string Content {get;set; }
        public string Picture {get;set; }
    }
}
