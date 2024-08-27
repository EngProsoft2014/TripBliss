using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Helpers
{
    public static class AirportInfo
    {
        public class AirLines
        {
            public string? iata_code { get; set; }
            public string? name { get; set; }
            public string? icao_code { get; set; }
        }

        public static List<AirLines> LstAirLines { get; set; } = new List<AirLines>();


        public async static Task<string> GetAirportInfo()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://iata-and-icao-codes.p.rapidapi.com/airlines"),
                Headers =
                        {
                            { "x-rapidapi-key", "6fccfd7c71msh7934183b73f2229p11ce70jsn0061fc86c83f" },
                            { "x-rapidapi-host", "iata-and-icao-codes.p.rapidapi.com" },
                        },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        //public async static Task<string> GetAirportInfo()
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri("https://airport-info.p.rapidapi.com/airport"),
        //        Headers =
        //                {
        //                    { "x-rapidapi-key", "6fccfd7c71msh7934183b73f2229p11ce70jsn0061fc86c83f" },
        //                    { "x-rapidapi-host", "airport-info.p.rapidapi.com" },
        //                },
        //    };
        //    using (var response = await client.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var body = await response.Content.ReadAsStringAsync();
        //        return body;
        //    }
        //}




    }
}
