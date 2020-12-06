using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using Dictant.Server.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Dictant.Server.Middleware
{
    public class RequireCaptchaAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var _captchaValidator = context.HttpContext.RequestServices.GetService<ICaptchaValidator>();
            try
            {
                if (context.HttpContext.Request.Query.ContainsKey("CaptchaToken"))
                {
                    context.HttpContext.Request.Query.TryGetValue("CaptchaToken", out StringValues token);
                     _captchaValidator.Check(token).Wait();
                }
                else
                {
                    throw new Exception("Captcha Required");
                }
            }
            catch (Exception e)
            {
                context.Result = new BadRequestObjectResult(e.Message);
            }
            base.OnResultExecuting(context);
        }
    }
}
