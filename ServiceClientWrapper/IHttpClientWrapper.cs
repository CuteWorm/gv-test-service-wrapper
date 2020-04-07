using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceClientWrapper
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> GetAsync(string uri, CancellationToken cancellation);
        Task<HttpResponseMessage> GetAsync(string uri, IDictionary<string, string> headers);
        Task<HttpResponseMessage> GetAsync(string uri, IDictionary<string, string> headers, CancellationToken cancellation);

        /// <summary>
        /// Send GET request to uri and convert response content to a specified type.
        /// Returns default(T) if response status code is not a success code.
        /// </summary>
        Task<T> GetAsync<T>(string uri);
        /// <summary>
        /// Send GET request to uri and convert response content to a specified type.
        /// Returns default(T) if response status code is not a success code.
        /// </summary>
        Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken);
        /// <summary>
        /// Send GET request to uri and convert response content to a specified type.
        /// Returns default(T) if response status code is not a success code.
        /// </summary>
        Task<T> GetAsync<T>(string uri, IDictionary<string, string> headers);
        /// <summary>
        /// Send GET request to uri and convert response content to a specified type.
        /// Returns default(T) if response status code is not a success code.
        /// </summary>
        Task<T> GetAsync<T>(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken);

        Task<HttpResponseMessage> PostAsync(string uri, HttpContent requestContent);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent requestContent, CancellationToken cancellationToken);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent requestContent, IDictionary<string, string> headers);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent requestContent, IDictionary<string, string> headers, CancellationToken cancellationToken);

        /// <summary>
        /// Serialize an object to JSON content and send POST request to uri.
        /// </summary>
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T objectContent);
        /// <summary>
        /// Serialize an object to JSON content and send POST request to uri.
        /// </summary>
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T objectContent, CancellationToken cancellationToken);
        /// <summary>
        /// Serialize an object to JSON content and send POST request to uri.
        /// </summary>
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T objectContent, IDictionary<string, string> headers);
        /// <summary>
        /// Serialize an object to JSON content and send POST request to uri.
        /// </summary>
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T objectContent, IDictionary<string, string> headers, CancellationToken cancellationToken);
    }
}
