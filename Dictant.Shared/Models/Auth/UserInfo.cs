using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dictant.Shared.Models.Auth
{
    public class UserInfo
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage = "Password must contains at least 6 characters")]
        [RegularExpression(@"^.*(?=.*[a-zA-Z])(?=.*\d).*$",ErrorMessage = "Password must contains at least one latin letter and number")]
        public string Password { get; set; }
    }
}