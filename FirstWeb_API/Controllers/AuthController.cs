using AutoMapper;
using FirstWeb_API.Data;
using RoyalHotel.DTO;
using FirstWeb_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWeb_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                if (registrationRequestDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Registration Detail is Required"));
                }

                if (await _authService.IsEmailExistsAsync(registrationRequestDTO.Email))
                {
                    return Conflict(ApiResponse<object>.Conflict("Email already exists"));
                }
                var user = await _authService.RegisterAsync(registrationRequestDTO);
                if (user == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("User Registration Failed"));
                }
                //Auth Service
                var response = ApiResponse<UserDTO>.CreatedAt(user, "User Created Successfully");
                return CreatedAtAction(nameof(Register),response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.Error("An error occurred while processing the request", ex.Message));
            }
            
        }


        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                if (loginRequestDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Login Detail is Required"));
                }
                var loginResponse = await _authService.LoginAsync(loginRequestDTO);
                if (loginResponse == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("User Login Failed"));
                }
                //Auth Service
                var response = ApiResponse<LoginResponseDTO>.Ok(loginResponse, "User Logged-in Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.Error("An error occurred while processing the request", ex.Message));
            }

        }
    }
}
