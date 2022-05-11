using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Api.Test.Common.Extentions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<JObject> ReadAsJson(this HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }

        public static async Task<T> ReadAsObject<T>(this HttpResponseMessage httpResponseMessage)
        {
            return await httpResponseMessage.Content.ReadAsAsync<T>();
        }
    }
}
