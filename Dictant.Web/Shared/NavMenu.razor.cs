using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictant.Web.Shared
{
    public partial class NavMenu
    {
        async Task LogoutUser()
        {
            await loginService.Logout();
            navigationManager.NavigateTo("");
        }
    }
}
