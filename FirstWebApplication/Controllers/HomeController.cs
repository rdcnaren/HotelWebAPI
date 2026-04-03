using AutoMapper;
using FirstWebApplication.Models;
using FirstWebApplication.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using RoyalHotel.DTO;
using System.Diagnostics;

namespace FirstWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly IMapper mapper;

        public HomeController(IHotelService hotelService, IMapper mapper)
        {
            this.hotelService = hotelService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<HotelDTO> Hotellist = new();
            try
            {
                var response = await hotelService.GetAllAsync<ApiResponse<List<HotelDTO>>>("");
                if(response != null && response.Success && response.Data != null)
                {
                    Hotellist = response.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An Error Occured : {ex.Message}";
            }
            return View(Hotellist);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
