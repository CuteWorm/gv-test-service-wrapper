using System;

namespace ServiceClientWrapper
{
    class Program
    {
        static IHttpClientWrapper _serviceWrapper;
        static void Main(string[] args)
        {
            _serviceWrapper = new HttpClientWrapper(new System.Net.Http.HttpClient());

            var location = _serviceWrapper.GetAsync<Location>("https://www.metaweather.com/api/location/862592/").Result;

            Console.WriteLine($"{location.Title} - {location.Time}");

            Console.ReadLine();
        }
    }
}
