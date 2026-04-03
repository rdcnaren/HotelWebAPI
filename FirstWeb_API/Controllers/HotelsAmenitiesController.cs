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
    [Route("api/Hotel-Amenities")]
    [ApiController]
    public class HotelsAmenitiesController : ControllerBase
    {

        private readonly ApplicationDbContext _LocalDb;

        private readonly IMapper _mapper;

        public HotelsAmenitiesController(ApplicationDbContext LocalDb,IMapper mapper)
        {
            _LocalDb = LocalDb;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task <ActionResult<ApiResponse<IEnumerable<HotelAmenitiesDTO>>>> GetAllHotelAmenities()
        {
            var hotelsamenities = await _LocalDb.HotelAmenities.ToListAsync();
            var dtoresponse = _mapper.Map<List<HotelAmenitiesDTO>>(hotelsamenities);
            var response = ApiResponse<IEnumerable<HotelAmenitiesDTO>>.Ok(dtoresponse, "Data Retrived Successfully");
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<HotelAmenitiesDTO>>> GetHotelAmenitiesById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest(ApiResponse<HotelAmenitiesDTO>.BadRequest("Invalid hotel id."));
                }
                var hotelamenities = await _LocalDb.HotelAmenities.FirstOrDefaultAsync(x=>x.id==id);
                if(hotelamenities == null)
                {
                    return NotFound(ApiResponse<HotelAmenitiesDTO>.NotFound($"Hotel Amenities with ID {id} not found."));
                }
                return Ok(ApiResponse<HotelAmenitiesDTO>.Ok(_mapper.Map<HotelAmenitiesDTO>(hotelamenities), "Data Retrived Successfully"));
                
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<HotelAmenitiesDTO>.Error("An error occurred while retrieving the hotel amenities.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<HotelAmenitiesCreateDTO>>> CreateHotelAmenities(HotelAmenitiesCreateDTO hotelamenitiesDTO)
        {
            try
            {
                if (hotelamenitiesDTO == null)
                {
                    return BadRequest(ApiResponse<HotelAmenitiesCreateDTO>.BadRequest("Hotel Amenities Details is Required"));
                }

                var Hotelexists = await _LocalDb.Hotel.Where(x => x.Id == hotelamenitiesDTO.HotelId).ToListAsync();
                if (Hotelexists == null)
                {
                    return Conflict(ApiResponse<HotelAmenitiesCreateDTO>.Conflict($"Another hotel with the Id '{hotelamenitiesDTO.HotelId}' already exists. Hotel Id must be unique."));
                }

                // Map the incoming DTO to the Hotel entity for persistence
                var hotelamenities = _mapper.Map<HotelAmenities>(hotelamenitiesDTO);
                hotelamenities.CreatedDate = DateTime.Now;

                await _LocalDb.HotelAmenities.AddAsync(hotelamenities);
                await _LocalDb.SaveChangesAsync();

                // Map the saved entity back to a DTO for the response
                var response = ApiResponse<HotelAmenitiesDTO>.CreatedAt(_mapper.Map<HotelAmenitiesDTO>(hotelamenities), "Hotel Amenities Created Successfully");
                return CreatedAtAction(nameof(CreateHotelAmenities), new { Id = hotelamenities.id }, response);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<HotelAmenitiesDTO>.Error("An error occurred while Creating the hotel amenities.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<HotelAmenitiesUpdateDTO>>> UpdateHotelAmenities(int id, HotelAmenitiesUpdateDTO hotelamenitiesupdateDTO)
        {
            try
            {
                if (hotelamenitiesupdateDTO == null)
                {
                    return BadRequest(ApiResponse<HotelAmenitiesUpdateDTO>.BadRequest("Hotel Amenities Details is Required"));
                }

                if (!id.Equals(hotelamenitiesupdateDTO.id))
                {
                    return NotFound(ApiResponse<HotelAmenitiesUpdateDTO>.NotFound("Hotel Amenities ID in the URL does not match the ID in the request body."));
                }

                var Hotelexists = await _LocalDb.Hotel.Where(x => x.Id == hotelamenitiesupdateDTO.HotelId).ToListAsync();
                if (Hotelexists == null)
                {
                    return Conflict(ApiResponse<HotelAmenitiesCreateDTO>.Conflict($"Another hotel with the Id '{hotelamenitiesupdateDTO.HotelId}' already exists. Hotel Id must be unique."));
                }

                var existingHotelamenities = await _LocalDb.HotelAmenities.FirstOrDefaultAsync(x => x.id == id);

                if(existingHotelamenities == null)
                {
                    return BadRequest(ApiResponse<HotelAmenitiesUpdateDTO>.BadRequest($"Hotel Amenities with ID {id} not found."));
                }

                _mapper.Map(hotelamenitiesupdateDTO, existingHotelamenities);
                existingHotelamenities.UpdatedDate = DateTime.Now;
                await _LocalDb.SaveChangesAsync();

                var dtoresponse = _mapper.Map<HotelAmenitiesDTO>(existingHotelamenities);
                var response = ApiResponse<HotelAmenitiesDTO>.Ok(dtoresponse, "Hotel Amenities Updated Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<HotelAmenitiesDTO>.Error("An error occurred while Updating the hotel.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteHotelAmenities(int id)
        {
            try
            {
                var existingHotelAmenities = await _LocalDb.HotelAmenities.FirstOrDefaultAsync(x => x.id == id);

                if (existingHotelAmenities == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Hotel Amenities with ID {id} not found."));
                }

                _LocalDb.HotelAmenities.Remove(existingHotelAmenities);

                await _LocalDb.SaveChangesAsync();
                var response = ApiResponse<object>.NoContent("Hotel Amenities Deleted Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<object>.Error("An error occurred while Deleting the Hotel Amenities.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }
    }
}
