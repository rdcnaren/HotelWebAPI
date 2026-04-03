using FirstWebApplication.Models;
using RoyalHotel.DTO;

namespace FirstWebApplication.Services.IServices
{
    public interface IBaseService
    {
        ApiResponse<object> ResponseModel { get; set; }
         Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
