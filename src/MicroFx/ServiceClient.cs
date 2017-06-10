using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace MicroFx
{
    public class ServiceClient : IServiceClient
    {
        public TResponse Get<TResponse>(string endpoint)
        {
            var responseObj = default(TResponse);

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.BaseAddress = new Uri("");
                

                client.GetAsync(endpoint)
                    .ContinueWith(t =>
                    {
                        var responseMsg = t.Result;
                        if (responseMsg.IsSuccessStatusCode)
                        {
                            var strResponse = responseMsg.Content.ReadAsStringAsync().Result;
                            responseObj = JsonConvert.DeserializeObject<TResponse>(strResponse);
                        }
                    });
            }
            return responseObj;
        }

        public TResponse Post<TRequest, TResponse>(string endpoint, TRequest request)
        {
            var responseObj = default(TResponse);

            var requestObj = new ObjectContent<TRequest>(request, new JsonMediaTypeFormatter());

            using (var client = new HttpClient())
            {
                client.PostAsync(endpoint, requestObj)
                    .ContinueWith(t =>
                    {
                        var responseMsg = t.Result;
                        if (responseMsg.IsSuccessStatusCode)
                        {
                            var strResponse = responseMsg.Content.ReadAsStringAsync().Result;
                            responseObj = JsonConvert.DeserializeObject<TResponse>(strResponse);
                        }
                    });
            }
            return responseObj;
        }
        public TResponse Put<TRequest, TResponse>(string endpoint, TRequest request)
        {
            var responseObj = default(TResponse);

            var requestObj = new ObjectContent<TRequest>(request, new JsonMediaTypeFormatter());

            using (var client = new HttpClient())
            {
                client.PutAsync(endpoint, requestObj)
                    .ContinueWith(t =>
                    {
                        var responseMsg = t.Result;
                        if (responseMsg.IsSuccessStatusCode)
                        {
                            var strResponse = responseMsg.Content.ReadAsStringAsync().Result;
                            responseObj = JsonConvert.DeserializeObject<TResponse>(strResponse);
                        }
                    });
            }
            return responseObj;
        }

        public TResponse Delete<TResponse>(string endpoint)
        {
            var responseObj = default(TResponse);

            using (var client = new HttpClient())
            {
                client.DeleteAsync(endpoint)
                    .ContinueWith(t =>
                    {
                        var responseMsg = t.Result;
                        if (responseMsg.IsSuccessStatusCode)
                        {
                            var strResponse = responseMsg.Content.ReadAsStringAsync().Result;
                            responseObj = JsonConvert.DeserializeObject<TResponse>(strResponse);
                        }
                    });
            }
            return responseObj;
        }
    }
}