using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TripBliss.Exceptions;
using Newtonsoft.Json;
using Polly;
using System.Text;
using System.Threading;
using TripBliss.Helpers;

namespace TripBliss.Helpers
{
    public interface IGenericRepository
    {
        public Task<T> GetAsync<T>(string uri, string authToken = "");
        public Task<string> GetStrAsync<T>(string uri, string authToken = "");
        public Task<string> GetStrMainAsync<T>(string uri, string authToken = "");
        public Task<T> PostAsync<T>(string uri, T data, string authToken = "");
        public Task<string> PostReturnString<T>(string uri, T data, string authToken = "");
        public Task<string> PostDataAsync<T>(string uri, T data, string authToken = "");
        public Task<string> PostMData<T>(string uri, T data, string authToken = "");
        public Task<string> PostMultiPicAsync<T>(string uri, T data, string authToken = "");
        public Task<T> PutAsync<T>(string uri, T data, string authToken = "");
        public Task<string> PutDataAsync<T>(string uri, T data, string authToken = "");
        public Task DeleteAsync(string uri, string authToken = "");
        public Task<string> DeleteItemAsync(string uri);
        public Task<R> PostAsync<T, R>(string uri, T data, string authToken = "");
    }


    public class GenericRepository : IGenericRepository
    {
        private HttpClient CreateHttpClient(string authToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            return httpClient;
        }

        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);
                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.GetAsync(Constants.ApiConstants.BaseApiUrl + uri));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult =
                        await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<string> GetStrAsync<T>(string uri, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);
                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.GetAsync(Constants.ApiConstants.BaseApiUrl + uri));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult =
                        await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return jsonResult;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new ServiceAuthenticationException(jsonResult);
                    return null;
                }

                //throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                return null;
            }
        }

        public async Task<string> GetStrMainAsync<T>(string uri, string authToken = "") // Without URL
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(uri);
                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.GetAsync(uri));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult =
                        await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return jsonResult;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new ServiceAuthenticationException(jsonResult);
                    return null;
                }

                //throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                return null;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Constants.ApiConstants.BaseApiUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<string> PostReturnString<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);

                string cc = JsonConvert.SerializeObject(data);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Constants.ApiConstants.BaseApiUrl + uri, content, CancellationToken.None));


                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return jsonResult;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new ServiceAuthenticationException(jsonResult);
                    return null;
                }

                //throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
                return null;

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                //return e.Message;
                return null;
            }
        }


        public async Task<string> PostDataAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                //var responseMessage= await httpClient.PostAsync(uri, content);

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Constants.ApiConstants.BaseApiUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<object>(jsonResult);
                    return json.ToString();
                }
                else
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    if (responseData.Contains("Not_Enough") == true)
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else if (responseMessage.ReasonPhrase == "Multiple Choices")
                    {
                        return "Multiple Choices";
                    }
                    else
                    {
                        return "api not responding";
                    }
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<string> PostMData<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Constants.ApiConstants.BaseApiUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<object>(jsonResult);
                    return json.ToString();
                }
                else if (responseMessage.ReasonPhrase == "Multiple Choices")
                {
                    return "Multiple Choices";
                }
                else
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    if (responseData.Contains("Not_Enough") == true)
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else if (responseData.Contains("already exists") == true)
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else if (responseData.Contains("The_invoice_exists") == true)
                    {
                        return "This Invoice Already Exist From Estimate Or Schedule";
                    }
                    else
                    {
                        return "api not responding";
                    }
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<string> PostMultiPicAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);

                var content = new StringContent(JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Constants.ApiConstants.BaseApiUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json.ToString();
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);

                string cc = JsonConvert.SerializeObject(data);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(Constants.ApiConstants.BaseApiUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<string> PutDataAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(Constants.ApiConstants.BaseApiUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<object>(jsonResult);
                    return json.ToString();
                }
                else
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    if (responseData.Contains("Not_Enough") == true)
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else
                    {
                        return "api not responding";
                    }
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            await httpClient.DeleteAsync(Constants.ApiConstants.BaseApiUrl + uri);
        }

        public async Task DeleteItemAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            await httpClient.DeleteAsync(Constants.ApiConstants.BaseApiUrl + uri);
        }

        public async Task<string> DeleteItemAsync(string uri)
        {
            var httpLient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            httpLient.DefaultRequestHeaders.Accept.Clear();
            httpLient.DefaultRequestHeaders.Add("authorization", "Basic ");

            try
            {
                response = await httpLient.DeleteAsync(Constants.ApiConstants.BaseApiUrl + uri);
                if (response.IsSuccessStatusCode)
                {
                    return response.ToString();
                }
                else
                    return "api not responding";
            }
            catch (Exception ex)
            {
                return "api not responding";
            }
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Constants.ApiConstants.BaseApiUrl + uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Constants.ApiConstants.BaseApiUrl + uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }


    }
}
