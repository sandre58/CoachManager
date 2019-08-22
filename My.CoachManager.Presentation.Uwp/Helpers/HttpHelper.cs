using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace My.CoachManager.Presentation.Uwp.Helpers
{
    public class HttpHelper
    {
        private static HttpClient _client;

        public static async Task<T> GetDataAsync<T>(string uri)
        {
            if (_client == null)
            {
                _client = new HttpClient { BaseAddress = new Uri("http://localhost:49301/") };
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            var exception = await response.Content.ReadAsAsync<Exception>();

            throw exception;
        }
    }
}
