using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceClientWrapper
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        private const string JSON_CONTENT = "application/json";

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
            => await GetAsync(uri, null, CancellationToken.None);
        public async Task<HttpResponseMessage> GetAsync(string uri, CancellationToken cancellationToken)
            => await GetAsync(uri, null, cancellationToken);
        public async Task<HttpResponseMessage> GetAsync(string uri, IDictionary<string, string> headers)
            => await GetAsync(uri, headers, CancellationToken.None);
        public async Task<HttpResponseMessage> GetAsync(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            var request = CreateRequest(HttpMethod.Get, uri, headers: headers);
            return await SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Send GET request to uri and convert response content to a specified type.
        /// Returns default(T) if response status code is not a success code.
        /// </summary>
        public async Task<T> GetAsync<T>(string uri)
            => await GetAsync<T>(uri, null, CancellationToken.None);
            /// <summary>
        /// Send GET request to uri and convert response content to a specified type.
        /// Returns default(T) if response status code is not a success code.
        /// </summary>
        public async Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken)
            => await GetAsync<T>(uri, null, cancellationToken);
            /// <summary>
        /// Send GET request to uri and convert response content to a specified type.
        /// Returns default(T) if response status code is not a success code.
        /// </summary>
        public async Task<T> GetAsync<T>(string uri, IDictionary<string, string> headers)
            => await GetAsync<T>(uri, headers, CancellationToken.None);
        /// <summary>
        /// Send GET request to uri and convert response content to a specified type.
        /// Returns default(T) if response status code is not a success code.
        /// </summary>
        public async Task<T> GetAsync<T>(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            var response = await GetAsync(uri, headers, cancellationToken);
            return response?.IsSuccessStatusCode == true ? JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync()) : default(T);
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent requestContent)
            => await PostAsync(uri, requestContent, null, CancellationToken.None);
        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent requestContent, CancellationToken cancellationToken)
            => await PostAsync(uri, requestContent, null, cancellationToken);
        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent requestContent, IDictionary<string, string> headers)
            => await PostAsync(uri, requestContent, headers, CancellationToken.None);
        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent requestContent, IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            var request = CreateRequest(HttpMethod.Post, uri, content: requestContent, headers: headers);
            return await SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Serialize an object to JSON content and send POST request to uri.
        /// </summary>
        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T objectContent)
            => await PostAsJsonAsync<T>(uri, objectContent, null, CancellationToken.None);
        /// <summary>
        /// Serialize an object to JSON content and send POST request to uri.
        /// </summary>
        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T objectContent, CancellationToken cancellationToken)
            => await PostAsJsonAsync<T>(uri, objectContent, null, cancellationToken);
        /// <summary>
        /// Serialize an object to JSON content and send POST request to uri.
        /// </summary>
        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T objectContent, IDictionary<string, string> headers)
            => await PostAsJsonAsync<T>(uri, objectContent, headers, CancellationToken.None);
        /// <summary>
        /// Serialize an object to JSON content and send POST request to uri.
        /// </summary>
        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T objectContent, IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonSerializer.Serialize(objectContent), Encoding.UTF8, JSON_CONTENT);
            return await PostAsync(uri, content, headers, cancellationToken);
        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            var response = cancellationToken == CancellationToken.None ?
                await _httpClient.SendAsync(requestMessage) :
                await _httpClient.SendAsync(requestMessage, cancellationToken);
            return response;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string uri, HttpContent content = null, IDictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(method, uri);
            if (content != null)
            {
                request.Content = content;
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            return request;
        }
    }
}
