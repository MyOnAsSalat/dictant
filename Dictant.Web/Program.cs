using Dictant.Web.Auth;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
namespace Dictant.Web
{
  public class Program
  {
    public static async System.Threading.Tasks.Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("app");
        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore(options =>
        {

        });
        builder.Services.AddScoped<JWTAuthenticationProvider>();
        builder.Services.AddSingleton<DialogService>();
        builder.Services.AddSingleton<NotificationService>();
        builder.Services.AddScoped<AuthenticationStateProvider,JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());
        builder.Services.AddScoped<ILoginService,JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());
        await builder.Build().RunAsync();
    }
  }
}
