using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using StatlerWaldorfCorp.TeamService.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
    public class HttpLocationClient : ILocationClient
    {
        public static String URL {get; set;}

        public async Task<LocationRecord> GetLatestForMember(Guid memberId) 
        {
            LocationRecord locationRecord = null;

            using (var httpClient = new HttpClient())
            {
                Console.WriteLine("-------------");
                Console.WriteLine(URL);

                httpClient.BaseAddress = new Uri(URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(String.Format("/locations/{0}/latest", memberId));

                if (response.IsSuccessStatusCode) {
                    string json = await response.Content.ReadAsStringAsync();
                    locationRecord = JsonConvert.DeserializeObject<LocationRecord>(json);
                }
            }

            return locationRecord;
        }

        public async Task<LocationRecord> AddLocation(Guid memberId, LocationRecord locationRecord) 
        {
            return locationRecord;
        }
    }
}