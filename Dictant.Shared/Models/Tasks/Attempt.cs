using System;
using System.Collections.Generic;
using System.Text;

namespace Dictant.Shared.Models.Tasks
{
    public class Attempt
    {
        public string Token { get; set; }
        public DateTimeOffset Start { get; set; }
        public bool Finished { get; set; }
        public DateTimeOffset End { get; set; }
        public string Result { get; set; }

    }
}
