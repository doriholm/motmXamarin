using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace motmXamarin.Data
{
    public class DataManager
    {
        const string Url = "https://motmv2.azurewebsites.net/umbraco/api/postsapi/";
        private string authorizationKey = "auth";

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            if (string.IsNullOrEmpty(authorizationKey))
            {
                authorizationKey = await client.GetStringAsync(Url + "login");
                authorizationKey = JsonConvert.DeserializeObject<string>(authorizationKey);
            }

            //client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Sport>> GetSports()
        {
            HttpClient client = await GetClient();
            string result = await client.GetStringAsync(Url + "getsports");
            return JsonConvert.DeserializeObject<IEnumerable<Sport>>(result);
        }

        public async Task<IEnumerable<Club>> GetClubs()
        {
            HttpClient client = await GetClient();
            var testString = "getclubs?sportsids=1084&sportsids=1093";

            string result = await client.GetStringAsync(Url + "getclubs?sportsids=1084&sportsids=1093");
            return JsonConvert.DeserializeObject<IEnumerable<Club>>(result);
        }

    }
}


