using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Clients.Interfaces;
using WebApp.Models;

namespace WebApp.Clients
{
    public class HttpTeamClient : ITeamClient
    {
        private readonly HttpClient httpClient;

        public HttpTeamClient(HttpClient client)
        {
            this.httpClient = client;
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task CreateTeamAsync(Team team)
        {
            var jsonRequest = JsonConvert.SerializeObject(team);
            HttpResponseMessage response =
              await httpClient.PostAsync(string.Empty, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteTeamAsync(Guid teamId)
        {
            HttpResponseMessage response =
              await httpClient.DeleteAsync(string.Format("{0}", teamId));
            // TO-DO: Indicate that we cannot delete it
        }

        public async Task<Team> GetTeamAsync(Guid teamId)
        {
            Team team = null;

            HttpResponseMessage response = await httpClient.GetAsync(string.Format("{0}", teamId.ToString()));

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                team = JsonConvert.DeserializeObject<Team>(jsonResponse);
            }

            return team;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            ICollection<Team> teams = new List<Team>();

            HttpResponseMessage response = await httpClient.GetAsync(string.Empty);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                teams = JsonConvert.DeserializeObject<List<Team>>(jsonResponse);
            }

            return teams;
        }

        public async Task UpdateTeamAsync(Team team)
        {
            var jsonRequest = JsonConvert.SerializeObject(team);
            HttpResponseMessage response =
              await httpClient.PutAsync(string.Format("{0}", team.TeamId), new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
