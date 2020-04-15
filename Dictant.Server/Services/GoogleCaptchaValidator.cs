﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Dictant.Server.Services
{
    public class GoogleCaptchaValidator : ICaptchaValidator
    {
        private string _secret;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GoogleCaptchaValidator(string secret, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _secret = secret;
            this.httpClientFactory = httpClientFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task Check(string token)
        {
            var webClient = this.httpClientFactory.CreateClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secret),
                new KeyValuePair<string, string>("response", token),
                new KeyValuePair<string, string>("remoteip", httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString())
            });
            var res = await webClient.PostAsync("https://www.google.com/recaptcha/api/siteverify",content);
            var resJson = JObject.Parse(await res.Content.ReadAsStringAsync());
            var succeed = resJson["success"].ToString() == "True";
            if (!succeed)
                throw new InvalidOperationException("Captcha validation failed : " + string.Join(",", resJson["error-codes"].AsEnumerable()));
        }
    }
}
