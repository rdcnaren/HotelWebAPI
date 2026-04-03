using FirstWebApplication.Models;
using FirstWebApplication.Services.IServices;
using RoyalHotel.DTO;

namespace FirstWebApplication.Services
{
    public class HotelService : BaseService, IHotelService
    {
        private const string APIEndPoint = "api/Hotel";
        public HotelService(IHttpClientFactory httpClient,IConfiguration configuration) : base(httpClient)
        {
        }

        public async Task<T?> CreateAsync<T>(HotelCreateDTO dto, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                data = dto,
                url = APIEndPoint,
                token = token
            });
        }

        public Task DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.DELETE,
                url = $"{APIEndPoint}/{id}",
                token = token
            });
        }

        public async Task<T?> GetAllAsync<T>(string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                url = $"{APIEndPoint}",
                token = token
            });
        }

        public async Task<T?> GetIdAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                url = $"{APIEndPoint}/{id}",
                token = token
            });
        }

        public async Task<T?> UpdateAsync<T>(HotelUpdateDTO dto, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.PUT,
                data = dto,
                url = $"{APIEndPoint}/{dto.Id}",
                token = token
            });
        }
    }
}
