using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Shared.Models.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Components.Moderation
{
    public partial class ModerationItemComponent
    {
        [Parameter]
        public DictantSource Source { get; set; }
    }
}
