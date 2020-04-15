using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dictant.Web.Extensions
{
    public static class HttpClientSafeJsonExtensions
    {
        public static EnrichableHttpClient Extend(this HttpClient httpClient)
        {
            return new EnrichableHttpClient(httpClient);
        }
        public static async Task<T> GetJsonAsync<T>(this EnrichableHttpClient httpClient, string requestUri)
        {
            return JsonSerializer.Deserialize<T>(await httpClient.httpClient.GetStringAsync(requestUri),
                JsonSerializerOptionsProvider.Options);
        }

        public static Task PostJsonAsync(
            this EnrichableHttpClient httpClient,
            string requestUri,
            object content)
        {
            return httpClient.SendJsonAsync(HttpMethod.Post, requestUri, content);
        }

        public static Task<T> PostJsonAsync<T>(
            this EnrichableHttpClient httpClient,
            string requestUri,
            object content)
        {
            return httpClient.SendJsonAsync<T>(HttpMethod.Post, requestUri, content);
        }

        public static Task PutJsonAsync(
            this EnrichableHttpClient httpClient,
            string requestUri,
            object content)
        {
            return httpClient.SendJsonAsync(HttpMethod.Put, requestUri, content);
        }

        public static Task<T> PutJsonAsync<T>(
            this EnrichableHttpClient httpClient,
            string requestUri,
            object content)
        {
            return httpClient.SendJsonAsync<T>(HttpMethod.Put, requestUri, content);
        }

        public static Task SendJsonAsync(
            this EnrichableHttpClient httpClient,
            HttpMethod method,
            string requestUri,
            object content)
        {
            return HttpClientJsonExtensions.SendJsonAsync<IgnoreResponse>(httpClient.httpClient, method, requestUri, content);
        }

        public static async Task<T> SendJsonAsync<T>(
            this EnrichableHttpClient httpClient,
            HttpMethod method,
            string requestUri,
            object content)
        {
            var content1 = JsonSerializer.Serialize(content, JsonSerializerOptionsProvider.Options);
            var httpResponseMessage = await httpClient.httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(content1, Encoding.UTF8, "application/json")
            });

            httpResponseMessage.EnsureSuccesStatusCodeEnriched();
            return typeof(T) == typeof(IgnoreResponse)
                ? default
                : JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(),
                    JsonSerializerOptionsProvider.Options);
        }

        private class IgnoreResponse
        {
        }
    }

    internal static class JsonSerializerOptionsProvider
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    public class HttpRequestEnrichedException : HttpRequestException
    {
        public readonly HttpResponseMessage Response;

        internal bool AllowRetry { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class.</summary>
        public HttpRequestEnrichedException(HttpResponseMessage response)
            : this((string) null, (Exception) null,response)
        {
            Response = response;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class with a specific message that describes the current exception.</summary>
        /// <param name="message">A message that describes the current exception.</param>
        public HttpRequestEnrichedException(string message,HttpResponseMessage response)
            : this(message, (Exception) null, response)
        {
            Response = response;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class with a specific message that describes the current exception and an inner exception.</summary>
        /// <param name="message">A message that describes the current exception.</param>
        /// <param name="inner">The inner exception.</param>
        public HttpRequestEnrichedException(string message, Exception inner,HttpResponseMessage response)
            : base(message, inner)
        {
            if (inner == null)
                return;
            this.HResult = inner.HResult;
            Response = response;
        }

        internal HttpRequestEnrichedException(string message, Exception inner, bool allowRetry,HttpResponseMessage response)
            : this(message, inner,response)
        {
            this.AllowRetry = allowRetry;
            Response = response;
        }
    }

    public static class HttpResponseExtension
    {
        public static HttpResponseMessage EnsureSuccesStatusCodeEnriched(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestEnrichedException(response);
            return response;
        }
    }

    public class EnrichableHttpClient
    {
        public HttpClient httpClient;
        public EnrichableHttpClient(HttpClient client)
        {
            httpClient = client;
        }
    }
}