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
        public BlogPost SourceExample = new BlogPost {Id = 0,Title="Title of post",Content="Text of post", Picture="https://avatars.mds.yandex.net/get-zen_doc/35845/pub_5a9bdeb5dcaf8e72a316cc59_5a9d008f79885e0afce66290/scale_1200"};
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
