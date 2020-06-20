using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Shared.Models.Blog;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Dictant.Web.Components.Feed
{
    public partial class BlogComponent
    {
        private EditContext NewPostContext;
        private readonly BlogPost NewPost = new BlogPost();
        private IEnumerable<BlogPost> Posts;
        protected override async Task OnInitializedAsync()
        {
            NewPostContext = new EditContext(NewPost);
            Posts = await http.GetJsonAsync<IEnumerable<BlogPost>>("https://localhost:5001/api/blog/get");
        }

        private async Task CreatePost()
        {
            await http.PostJsonAsync("https://localhost:5001/api/blog/post", NewPost);
            navigationManager.NavigateTo("");
        }
    }
}
