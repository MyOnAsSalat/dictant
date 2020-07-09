using System;
using System.Collections.Generic;
using System.Text;

namespace Dictant.Shared.Models.Tasks
{
    public class UserAttempt
    {
        public string Title { get; set; }
        public Attempt Attempt { get; set; }
    }
}
