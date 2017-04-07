using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TelegramBot.Util;

namespace TelegramBot.API
{
    public class ApiClient
    {
        string BaseApiAddress = @"https://api.telegram.org/bot";

        private readonly IRestClient _client;

        public ApiClient(string token)
        {
            _client = new RestClient(BaseApiAddress + token);
        }
        
        public async Task<TResult> SendRequestAsync<TResult>(string method, object obj = null)
        {
            var request = new RestRequest(method, Method.POST) {RequestFormat = DataFormat.Json};
            if (obj != null)
            {
                request.AddHeader("Content-type", "application/json");
                request.JsonSerializer = NewtonsoftJsonSerializer.Default;
                request.AddJsonBody(obj);
            }
            var result = await _client.ExecutePostTaskAsync(request);
            return JsonConvert.DeserializeObject<TResult>(result.Content);
        }
    }
}
