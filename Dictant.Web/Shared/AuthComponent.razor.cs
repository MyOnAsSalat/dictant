using System;
using System.Threading.Tasks;
using Dictant.Shared.Models;
using Dictant.Web.Extensions;
using Dictant.Web.Helpers;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;

namespace Dictant.Web.Shared
{
    public partial class AuthComponent
    {
        private readonly UserInfo userInfo = new UserInfo();
        private bool isLoading;
        private EditContext RegisterContext;

        protected override Task OnInitializedAsync()
        {
            RegisterContext = new EditContext(userInfo);
            return base.OnInitializedAsync();
        }

        private async Task CreateUser()
        {
            isLoading = true;
            if (!RegisterContext.Validate()) return;
            try
            {
                var token = await js.RunReCaptcha("register");
                var result = await http.Extend().PostJsonAsync<UserToken>("https://localhost:5001/api/accounts/create?CaptchaToken=" + token, userInfo);
                await loginService.Login(result.Token);
                navigationManager.NavigateTo("");
            }
            catch (HttpRequestEnrichedException e)
            {
                var result = await e.Response.Content.ReadAsStringAsync();
                switch (result)
                {
                    case "Failed : DuplicateUserName":
                        notifyService.Notify(NotificationSeverity.Error, "Registration failed", "Account already exist", 10000);
                        break;
                }

                Console.WriteLine("Exception in request, response:");
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                notifyService.Notify(NotificationSeverity.Error, "Account creation failed", "Please try again or contact administrator", 10000);
                Console.WriteLine(e.Message);
            }
            isLoading = false;
        }

        private async Task ResetPassword()
        {
        }

        private async Task LoginUser(LoginArgs args)
        {
            isLoading = true;
            try
            {
                userInfo.Email = args.Username;
                userInfo.Password = args.Password;
                var token = await js.RunReCaptcha("login");
                var result = await http.Extend().PostJsonAsync<UserToken>("https://localhost:5001/api/accounts/login?CaptchaToken=" + token, userInfo);
                await loginService.Login(result.Token);
                navigationManager.NavigateTo("");
            }
            catch (HttpRequestEnrichedException e)
            {
                var result = await e.Response.Content.ReadAsStringAsync();
                if (result.Contains("Invalid login attempt."))
                    notifyService.Notify(NotificationSeverity.Error, "Failed to log in", "Please make sure that you have entered your login and password correctly", 10000);
                else
                    notifyService.Notify(NotificationSeverity.Error, "Authentication failed", "Please make sure that you have entered your login and password correctly or contact administrator", 10000);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                notifyService.Notify(NotificationSeverity.Error, "Authentication in failed", "Please try again or contact administrator", 10000);
                Console.WriteLine(e.Message);
            }
            isLoading = false;
        }
    }
}