using FirstWebApplication.Models;
using FirstWebApplication.Services.IServices;
using RoyalHotel.DTO;
using System.Text.Json;

namespace FirstWebApplication.Services
{
    public class BaseService : IBaseService
    {
        public IHttpClientFactory HttpClient{ get; set; }
        public ApiResponse<object> ResponseModel { get; set; }

        private static readonly JsonSerializerOptions JsonOption = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public BaseService(IHttpClientFactory httpClient)
        {
            HttpClient = httpClient;
            ResponseModel = new ApiResponse<object>();
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("HotelWebAPI");
                var message = new HttpRequestMessage
                {
                    RequestUri = new Uri(apiRequest.url, UriKind.Relative),
                    Method = GetHttpClientMethod(apiRequest.apiType)
                };

                if (apiRequest.data != null)
                    message.Content = JsonContent.Create(apiRequest.data, options: JsonOption);

                if (!string.IsNullOrEmpty(apiRequest.token))
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiRequest.token);

                var apiResponse = await client.SendAsync(message);
                return await apiResponse.Content.ReadFromJsonAsync<T>(JsonOption);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return default!;
            }
        }

        private static HttpMethod GetHttpClientMethod(SD.ApiType apiType)
        {
            return apiType switch
            {
                SD.ApiType.POST => HttpMethod.Post,
                SD.ApiType.PUT => HttpMethod.Put,
                SD.ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get
            };
        }
    }
}
