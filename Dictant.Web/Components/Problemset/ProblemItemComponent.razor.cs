using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Shared.Models.Blog;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Components.Problemset
{
    public partial class ProblemItemComponent
    {
        [Parameter]
        public DictantSource Source { get; set; }
    }
}
