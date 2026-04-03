using RoyalHotel.DTO;

namespace FirstWebApplication.Services.IServices
{
    public interface IHotelService
    {
        Task<T?>GetAllAsync<T>(string token);
        Task<T?> GetIdAsync<T>(int id, string token);
        Task<T?> CreateAsync<T>(HotelCreateDTO dto, string token);
        Task<T?> UpdateAsync<T>(HotelUpdateDTO dto, string token);
        Task DeleteAsync<T>(int id,string token);
    }
}
