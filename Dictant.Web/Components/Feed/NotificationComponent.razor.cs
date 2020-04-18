using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dictant.Web.Components.Feed
{
    public partial class NotificationComponent
    {
        private Claim[] Claims = new Claim[0];
        bool test = false;
        protected override async Task OnInitializedAsync()
        {
            Claims =  (await authStateProvider.GetAuthenticationStateAsync()).User.Claims.ToArray();
            (await authStateProvider.GetAuthenticationStateAsync()).User.IsInRole("moderator");
        }
    }
}
