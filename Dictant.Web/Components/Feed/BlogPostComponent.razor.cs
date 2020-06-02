using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Shared.Models.Blog;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Components.Feed
{
    public partial class BlogPostComponent
    {
        [Parameter]
        public BlogPost Source { get; set; }
    }
}
