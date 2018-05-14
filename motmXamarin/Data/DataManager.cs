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
        private string authorizationKey = "auth"; //Change so it applies to user login method

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

        public async Task<IEnumerable<Club>> GetClubs(List<int> sportIds)
        {
            string searchString = "getclubs?";
            foreach(int id in sportIds){
                searchString += "sportsids=" + id + "&";
            }

            HttpClient client = await GetClient();
            string result = await client.GetStringAsync(Url + searchString);

            return JsonConvert.DeserializeObject<IEnumerable<Club>>(result);
        }

        public async Task<SingleClub> GetSingleClub(int clubId)
        {
            string searchString = "getsingleclub?clubid=" + clubId;


            HttpClient client = await GetClient();
            string result = await client.GetStringAsync(Url + searchString);

            return JsonConvert.DeserializeObject<SingleClub>(result);
        }
        
        //create a voting API
		public async Task<string> VoteMotm(int playerId)
        {
			string searchString = "playervote?playerid=" + playerId;
            

            HttpClient client = await GetClient();
			string result = await client.GetStringAsync(Url + searchString);

			return result;
        }

    }
}


