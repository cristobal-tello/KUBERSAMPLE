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
    public class HttpMemberClient : IMemberClient
    {
        private readonly HttpClient httpClient;

        public HttpMemberClient(HttpClient client)
        {
            this.httpClient = client;
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task CreateMemberAsync(Member member)
        {
            var jsonString = JsonConvert.SerializeObject(member);
            HttpResponseMessage response =
              await httpClient.PostAsync(string.Empty, new StringContent(jsonString, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteMemberAsync(Guid memberId)
        {
            HttpResponseMessage response =
              await httpClient.DeleteAsync(string.Format("{0}", memberId));
            // TO-DO: Indicate that we cannot delete it
        }

        public async Task<Member> GetMemberAsync(Guid memberId)
        {
            Member member = null;

            HttpResponseMessage response = await httpClient.GetAsync(string.Format("{0}", memberId.ToString()));

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                member = JsonConvert.DeserializeObject<Member>(json);
            }

            return member;
        }

        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            List<Member> members = new List<Member>();

            HttpResponseMessage response = await httpClient.GetAsync(string.Empty);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                members = JsonConvert.DeserializeObject<List<Member>>(json);
            }

            return members;
        }

        public async Task UpdateMemberAsync(Member member)
        {
            var jsonString = JsonConvert.SerializeObject(member);
            HttpResponseMessage response =
              await httpClient.PutAsync(string.Format("{0}", member.MemberId), new StringContent(jsonString, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
