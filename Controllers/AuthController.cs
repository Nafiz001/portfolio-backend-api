using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PortfolioAPI.Models;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                    return BadRequest(new LoginResponse 
                    { 
                        Success = false, 
                        Message = "Username and password are required" 
                    });

                if (_authService.ValidateCredentials(request.Username, request.Password))
                {
                    var token = _authService.GenerateJwtToken(request.Username);
                    
                    return Ok(new LoginResponse
                    {
                        Success = true,
                        Token = token,
                        Username = request.Username,
                        Message = "Login successful"
                    });
                }

                return Unauthorized(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponse
                {
                    Success = false,
                    Message = "An error occurred during login"
                });
            }
        }

        [HttpPost("verify")]
        public ActionResult VerifyToken([FromBody] TokenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Token))
                    return BadRequest(new { message = "Token is required" });

                var principal = _authService.ValidateJwtToken(request.Token);
                
                if (principal == null)
                    return Unauthorized(new { message = "Invalid or expired token" });

                var username = principal.Identity?.Name;
                
                return Ok(new { 
                    valid = true, 
                    username = username,
                    message = "Token is valid"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error validating token" });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            // In a real application, you might want to blacklist the token
            // For JWT tokens, logout is typically handled client-side by removing the token
            return Ok(new { message = "Logout successful" });
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            try
            {
                var username = User.Identity?.Name;
                return Ok(new { username = username });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving user information" });
            }
        }
    }
}
