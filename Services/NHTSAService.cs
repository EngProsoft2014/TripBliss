// Services/NHTSAService.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TripBliss.Services
{
    public class NHTSAService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://vpic.nhtsa.dot.gov/api/vehicles/";

        public NHTSAService()
        {
            //_httpClient = httpClient;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "TripBliss-App");
        }

        // نموذج البيانات القادمة من NHTSA
        public class NHTSAResponse<T>
        {
            public int Count { get; set; }
            public string Message { get; set; }
            public List<T> Results { get; set; }
        }

        public class MakeResult
        {
            public int Make_ID { get; set; }
            public string Make_Name { get; set; }
        }

        public class ModelResult
        {
            public int Model_ID { get; set; }
            public string Model_Name { get; set; }
        }

        //// جلب جميع الماركات
        //public async Task<List<MakeResult>> GetAllMakesAsync()
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetAsync($"{BaseUrl}/GetAllMakes?format=json");
        //        response.EnsureSuccessStatusCode();

        //        var json = await response.Content.ReadAsStringAsync();
        //        var result = JsonSerializer.Deserialize<NHTSAResponse<MakeResult>>(json);

        //        return result?.Results ?? new List<MakeResult>();
        //    }
        //    catch (Exception ex)
        //    {
        //        // تسجيل الخطأ
        //        Console.WriteLine($"Error fetching makes: {ex.Message}");
        //        return new List<MakeResult>();
        //    }
        //}

        //// جلب الموديلات لماركة وسنة معينة
        //public async Task<List<ModelResult>> GetModelsForMakeYearAsync(string make, int year)
        //{
        //    try
        //    {
        //        var url = $"{BaseUrl}/GetModelsForMakeYear/make/{make}/modelyear/{year}?format=json";
        //        var response = await _httpClient.GetAsync(url);
        //        response.EnsureSuccessStatusCode();

        //        var json = await response.Content.ReadAsStringAsync();
        //        var result = JsonSerializer.Deserialize<NHTSAResponse<ModelResult>>(json);

        //        return result?.Results ?? new List<ModelResult>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error fetching models: {ex.Message}");
        //        return new List<ModelResult>();
        //    }
        //}

        //// جلب الموديلات باستخدام Make ID
        //public async Task<List<ModelResult>> GetModelsForMakeIdYearAsync(int makeId, int year)
        //{
        //    try
        //    {
        //        var url = $"{BaseUrl}/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json";
        //        var response = await _httpClient.GetAsync(url);
        //        response.EnsureSuccessStatusCode();

        //        var json = await response.Content.ReadAsStringAsync();
        //        var result = JsonSerializer.Deserialize<NHTSAResponse<ModelResult>>(json);

        //        return result?.Results ?? new List<ModelResult>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error fetching models by MakeId: {ex.Message}");
        //        return new List<ModelResult>();
        //    }
        //}



        /// <summary>
        /// جلب جميع الماركات من NHTSA API
        /// </summary>
        public async Task<List<MakeResult>> GetAllMakesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("GetAllMakes?format=json");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<NHTSAResponse<MakeResult>>(json);

                return result?.Results ?? new List<MakeResult>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error fetching makes: {ex.Message}");
                return new List<MakeResult>();
            }
        }

        /// <summary>
        /// جلب الموديلات لماركة وسنة معينة
        /// </summary>
        public async Task<List<ModelResult>> GetModelsForMakeYearAsync(string make, int year)
        {
            try
            {
                var url = $"GetModelsForMakeYear/make/{make}/modelyear/{year}?format=json";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<NHTSAResponse<ModelResult>>(json);

                return result?.Results ?? new List<ModelResult>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error fetching models for {make} {year}: {ex.Message}");
                return new List<ModelResult>();
            }
        }

        /// <summary>
        /// التخلص من الموارد
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
