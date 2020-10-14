using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApp.Services.API
{
    public class RestService
    {
        private HttpClient _httpClient;
        private readonly string jsonMediaType = "application/json";
        private readonly string _endPoint;

        public RestService(string endPoint)
        {
            _endPoint = endPoint;
            _httpClient = MakeHttpClient(null);
        }

        protected virtual HttpClient MakeHttpClient(HttpMessageHandler handler)
        {
            if (handler != null)
            {
                _httpClient = new HttpClient(handler);
            }
            else
            {
                _httpClient = new HttpClient();
            }

            _httpClient.MaxResponseContentBufferSize = 1656000; // 256000
            _httpClient.Timeout = TimeSpan.FromMilliseconds(120000);
            _httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(jsonMediaType));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("defalte"));

            return _httpClient;
        }

        public async Task<HttpResponseMessage> PostAsyncForm(string method, Dictionary<string, string> parameters)
        {
            var form = new FormUrlEncodedContent(parameters);
            var uri = new Uri(_endPoint + method);
            var response = await _httpClient.PostAsync(uri, form);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string method, string json)
        {
            return await PostAsync(method, null, json);
        }

        public async Task<HttpResponseMessage> PutAsync(string method, Dictionary<string, string> parameters)
        {
            var uri = new Uri(BuidlQueryString((_endPoint + method), parameters));
            return await _httpClient.PutAsync(uri, null);
        }

        public async Task<HttpResponseMessage> PostAsync(string method, Dictionary<string, string> parameters, string json)
        {
            var uri = new Uri(BuidlQueryString((_endPoint + method), parameters));
            if (string.IsNullOrEmpty(json))
            {

                return await _httpClient.PostAsync(uri, null);
            }
            else
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await _httpClient.PostAsync(uri, content);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string method, Dictionary<string, string> parameters)
        {
            var uri = new Uri(BuidlQueryString((_endPoint + method), parameters));
            return await _httpClient.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> GetAsyncInfoProvider(string method, Dictionary<string, string> parameters)
        {
            var newEndPoint = _endPoint.TrimEnd('/');
            var uri = new Uri(BuidlQueryString(newEndPoint, parameters));
            return await _httpClient.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string method, Dictionary<string, string> parameters)
        {
            var uri = new Uri(BuidlQueryString((_endPoint + method), parameters));
            return await _httpClient.DeleteAsync(uri);
        }

        private string BuidlQueryString(string endPoint, Dictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                return string.Format("{0}?{1}", endPoint, string.Join("&", parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
            }
            else
            {
                return endPoint;
            }
        }

        public HttpClient Client { get { return _httpClient; } }
    }
}
