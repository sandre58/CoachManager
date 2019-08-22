using My.CoachManager.CrossCutting.Core.Exceptions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace My.CoachManager.Presentation.Core.Helpers
{
    public static class ApiHelper
    {
        private static HttpClient _client;

        /// <summary>
        /// Initialise HttpClient.
        /// </summary>
        /// <param name="serverUrl"></param>
        public static void InitializeHttpClient(string serverUrl)
        {
            _client = new HttpClient(new HttpClientHandler
            {
                UseDefaultCredentials = true,
                AutomaticDecompression = DecompressionMethods.GZip
            })
            { BaseAddress = new Uri(serverUrl) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
        }

        /// <summary>
        /// Get Data from web service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<T> GetDataAsync<T>(string uri, params object[] parameters)
        {
            if (_client == null)
            {
                throw new Exception("HttpClient has not been initialized. Call HttpHelper.InitializeHttpClient([url]) before use this method.");
            }

            var paramsStr = parameters != null ? String.Join("/", parameters) : string.Empty;
            var response = await _client.GetAsync(uri + (string.IsNullOrEmpty(paramsStr) ? string.Empty : "/" + paramsStr));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            var exception = await response.Content.ReadAsAsync<ApiException>();

            throw exception;
        }

        /// <summary>
        /// Get Data from web service.
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="uri"></param>
        /// <param name="value"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<TReturn> PostDataAsync<TParam, TReturn>(string uri, TParam value, params object[] parameters)
        {
            if (_client == null)
            {
                throw new Exception("HttpClient has not been initialized. Call HttpHelper.InitializeHttpClient([url]) before use this method.");
            }

            var paramsStr = parameters != null ? String.Join("/", parameters) : string.Empty;
            var response = await _client.PostAsJsonAsync(uri + (string.IsNullOrEmpty(paramsStr) ? string.Empty : "/" + paramsStr), value);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TReturn>();
            }

            var exception = await response.Content.ReadAsAsync<ApiException>();

            throw exception;
        }

        /// <summary>
        /// Delete Data from web service.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteDataAsync(string uri, params object[] parameters)
        {
            if (_client == null)
            {
                throw new Exception("HttpClient has not been initialized. Call HttpHelper.InitializeHttpClient([url]) before use this method.");
            }

            var paramsStr = parameters != null ? string.Join("/", parameters) : string.Empty;
            var response = await _client.DeleteAsync(uri + (string.IsNullOrEmpty(paramsStr) ? string.Empty : "/" + paramsStr));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var exception = await response.Content.ReadAsAsync<ApiException>();

            throw exception;
        }

        /// <summary>
        /// Get Data from web service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T GetData<T>(string uri, params object[] parameters)
        {
            return GetDataAsync<T>(uri, parameters).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get Data from web service.
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="uri"></param>
        /// <param name="value"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static TReturn PostData<TParam, TReturn>(string uri, TParam value, params object[] parameters)
        {
            return PostDataAsync<TParam, TReturn>(uri, value, parameters).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get Data from web service.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool DeleteData(string uri, params object[] parameters)
        {
            return DeleteDataAsync(uri, parameters).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get Data from web service.
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="uri"></param>
        /// <param name="value"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static void PostData<TParam>(string uri, TParam value, params object[] parameters)
        {
            PostDataAsync<TParam, bool>(uri, value, parameters).GetAwaiter().GetResult();
        }
    }
}