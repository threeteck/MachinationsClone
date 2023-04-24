using System.Linq;
using System.Threading.Tasks;
using MachinationsClone.Models;
using MachinationsClone.Models.DTOs;
using MachinationsClone.Models.DTOs.Request;
using MachinationsClone.Models.DTOs.Response;
using MachinationsClone.Models.Entities;
using MachinationsClone.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachinationsClone.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController
    {
        private readonly ApplicationContext _context;
        private readonly AuthenticationService _authenticationService;
        
        public AccountController(ApplicationContext context, AuthenticationService authenticationService)
        {
            _context = context;
            _authenticationService = authenticationService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (existingUser != null)
            {
                return new BadRequestObjectResult("Username already exists");
            }
            
            var user = new User
            {
                Username = dto.Username
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            var token = _authenticationService.GetToken(user);
            
            var userDto = UserDto.FromUser(user, token);
            return new OkObjectResult(userDto);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null)
            {
                return new NotFoundResult();
            }

            var token = _authenticationService.GetToken(user);

            var userDto = UserDto.FromUser(user, token);
            return new OkObjectResult(userDto);
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return new OkResult();
        }
    }
}
