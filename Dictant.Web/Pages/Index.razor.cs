using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictant.Web.Pages
{
    public partial class Index
    {
        public  async Task Test()
        {
          var user = (await authStateProvider.GetAuthenticationStateAsync()).User;
          
        }
    }
}
