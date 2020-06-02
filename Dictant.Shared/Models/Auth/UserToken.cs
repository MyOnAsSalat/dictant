using System;
using System.Collections.Generic;
using System.Text;

namespace Dictant.Shared.Models.Auth
{
    //DTO
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}