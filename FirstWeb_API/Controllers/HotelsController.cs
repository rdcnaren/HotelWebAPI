using AutoMapper;
using FirstWeb_API.Data;
using FirstWeb_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.DTO;
using System.Collections;

namespace FirstWeb_API.Controllers
{
    [Route("api/Hotel")]
    [ApiController]
    public class HotelsController : ControllerBase
    {

        private readonly ApplicationDbContext _LocalDb;

        private readonly IMapper _mapper;

        public HotelsController(ApplicationDbContext LocalDb,IMapper mapper)
        {
            _LocalDb = LocalDb;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task <ActionResult<ApiResponse<IEnumerable<HotelDTO>>>> GetAllHotels()
        {
            var hotels = await _LocalDb.Hotel.ToListAsync();
            var dtoresponse = _mapper.Map<List<HotelDTO>>(hotels);
            var response = ApiResponse<IEnumerable<HotelDTO>>.Ok(dtoresponse, "Data Retrived Successfully");
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<HotelDTO>>> GetHotelById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest(ApiResponse<HotelDTO>.BadRequest("Invalid hotel id."));
                }
                var hotel = await _LocalDb.Hotel.FirstOrDefaultAsync(x=>x.Id==id);
                if(hotel == null)
                {
                    return NotFound(ApiResponse<HotelDTO>.NotFound($"Hotel with ID {id} not found."));
                }
                return Ok(ApiResponse<HotelDTO>.Ok(_mapper.Map<HotelDTO>(hotel), "Data Retrived Successfully"));
                
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<HotelDTO>.Error("An error occurred while retrieving the hotel.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<HotelCreateDTO>>> CreateHotel(HotelCreateDTO hotelDTO)
        {
            try
            {
                if (hotelDTO == null)
                {
                    return BadRequest(ApiResponse<HotelCreateDTO>.BadRequest("Hotel Details is Required"));
                }

                var duplicates = await _LocalDb.Hotel.Where(x => x.Name.ToLower() == hotelDTO.Name.ToLower()).ToListAsync();
                if (duplicates.Count > 0)
                {
                    return Conflict(ApiResponse<HotelCreateDTO>.Conflict($"Another hotel with the name '{hotelDTO.Name}' already exists. Hotel names must be unique."));
                }

                // Map the incoming DTO to the Hotel entity for persistence
                var hotel = _mapper.Map<Hotel>(hotelDTO);
                hotel.Created = DateTime.Now;

                await _LocalDb.Hotel.AddAsync(hotel);
                await _LocalDb.SaveChangesAsync();

                // Map the saved entity back to a DTO for the response
                var response = ApiResponse<HotelDTO>.CreatedAt(_mapper.Map<HotelDTO>(hotel), "Hotel Created Successfully");
                return CreatedAtAction(nameof(CreateHotel), new { id = hotel.Id }, response);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<HotelDTO>.Error("An error occurred while Creating the hotel.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<HotelUpdateDTO>>> UpdateHotel(int id, HotelUpdateDTO hotelDTO)
        {
            try
            {
                if (hotelDTO == null)
                {
                    return BadRequest(ApiResponse<HotelUpdateDTO>.BadRequest("Hotel Details is Required"));
                }

                if (!id.Equals(hotelDTO.Id))
                {
                    return NotFound(ApiResponse<HotelUpdateDTO>.NotFound("Hotel ID in the URL does not match the ID in the request body."));
                }

                var existingHotel = await _LocalDb.Hotel.FirstOrDefaultAsync(x => x.Id == id);

                if(existingHotel == null)
                {
                    return BadRequest(ApiResponse<HotelUpdateDTO>.BadRequest($"Hotel with ID {id} not found."));
                }

                var duplicates = await _LocalDb.Hotel.Where(x => x.Name.ToLower() == hotelDTO.Name.ToLower() && x.Id != id).ToListAsync();
                if(duplicates.Count > 0)
                {
                    return Conflict(ApiResponse<HotelUpdateDTO>.BadRequest($"Another hotel with the name '{hotelDTO.Name}' already exists. Hotel names must be unique."));
                }
                _mapper.Map(hotelDTO, existingHotel);
                existingHotel.Updated = DateTime.Now;
                await _LocalDb.SaveChangesAsync();

                var dtoresponse = _mapper.Map<HotelDTO>(existingHotel);
                var response = ApiResponse<HotelDTO>.Ok(dtoresponse, "Hotel Updated Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<HotelDTO>.Error("An error occurred while Updating the hotel.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteHotel(int id)
        {
            try
            {
                var existingHotel = await _LocalDb.Hotel.FirstOrDefaultAsync(x => x.Id == id);

                if (existingHotel == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Hotel with ID {id} not found."));
                }

                _LocalDb.Hotel.Remove(existingHotel);

                await _LocalDb.SaveChangesAsync();
                var response = ApiResponse<object>.NoContent("Hotel Deleted Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<object>.Error("An error occurred while Deleting the hotel.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }
    }
}
