using EmployeeCatalog.Backend.Data.Models;
using EmployeeCatalog.Backend.Domain.Service;
using EmployeeCatalog.Backend.Domain.Service.Imp;
using EmployeeCatalogApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeCatalogApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService userService;

        public AuthController(UserManager<User> userManager, IOptions<JwtSettings> jwtOptions, IUserService userService)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                    return Unauthorized();

                var token = GenerateJwtToken(user);
                return Ok(new { token, user.Role });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterUserDto newUser)
        {
            try
            {
                if (string.IsNullOrEmpty(newUser.Username) || string.IsNullOrEmpty(newUser.Password))
                {
                    return BadRequest("Username y Password son requeridos.");
                }

                // Verificar si el usuario ya existe
                var existingUser = await userService.GetUserByUsernameAsync(newUser.Username);
                if (existingUser != null)
                {
                    return BadRequest("El usuario ya existe.");
                }

                // Crear el hash de la contraseña
                var passwordHash = userService.CreatePasswordHash(newUser.Password);

                var user = new User
                {
                    UserName = newUser.Username,
                    PasswordHash = passwordHash,
                    Role = MapRoleIntToString(newUser.Role)
                };

                await userService.RegisterUserAsync(user);

                return Ok(new { message = "Usuario registrado exitosamente." });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string MapRoleIntToString(int role)
        {
            return role switch
            {
                1 => "Admin",  // Mapeo de 1 a "Admin"
                2 => "User",   // Mapeo de 2 a "User"
                _ => "User"    // Valor por defecto
            };
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);  // Clave secreta para firmar el token

            // Crear los claims, incluyendo el rol del usuario
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  
                    new Claim(ClaimTypes.Name, user.UserName),                 
                    new Claim(ClaimTypes.Role, user.Role)                      
                }),
                Expires = DateTime.UtcNow.AddDays(7),  
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)  
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
