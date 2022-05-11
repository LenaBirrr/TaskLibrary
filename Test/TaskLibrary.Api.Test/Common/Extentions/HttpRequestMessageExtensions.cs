using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Api.Test.Common.Extentions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddJsonContent(this HttpRequestMessage message, JToken? json)
        {
            if (json != null)
                message.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            else
                message.Content = JsonContent.Create(null, typeof(object));
            return message;
        }

        public static HttpRequestMessage AddJwtToken(this HttpRequestMessage message, string? jwtToken)
        {
            if (jwtToken != null)
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            return message;
        }
    }
}
