using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using StatlerWaldorfCorp.TeamService.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
    public class HttpLocationClient : ILocationClient
    {
        public String URL { get; set; }

        public HttpLocationClient(string url)
        {
            this.URL = url;
        }

        public async Task<LocationRecord> GetLatestForMember(Guid memberId)
        {
            LocationRecord locationRecord = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(this.URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(String.Format("/locations/{0}/latest", memberId));

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    locationRecord = JsonConvert.DeserializeObject<LocationRecord>(json);
                }
            }

            return locationRecord;
        }

        public async Task<LocationRecord> AddLocation(Guid memberId, LocationRecord locationRecord)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(this.URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = JsonConvert.SerializeObject(locationRecord);
                var uri = String.Format("/locations/{0}", memberId);
                HttpResponseMessage response =
                  await httpClient.PostAsync(uri, new StringContent(jsonString, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                }
            }

            return locationRecord;
        }
    }
}