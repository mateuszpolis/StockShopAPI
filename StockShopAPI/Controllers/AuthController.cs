using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockShopAPI.Models;
using StockShopAPI.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Dapper;
using StockShopAPI.Helpers;
using StockShopAPI.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace StockShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private DataContext _context;
        private AuthRepository _authRepository;

        public AuthController(IConfiguration configuration, DataContext context, AuthRepository authRepository)
        {
            _configuration = configuration;
            _context = context;
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterAsync(UserRegisterDto request)
        {
            if (!ValidatePassword(request.Password))
            {
                return BadRequest("Password not valid");
            }
            if (!ValidateEmail(request.Email))
            {
                return BadRequest("Email not valid");
            }
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;

            await _authRepository.Create(user);

            return Ok("Registration succesfull");
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginDto request)
        {
            User user = await _authRepository.GetByEmail(request.Email);

            if (user == null)
            {
                return BadRequest("Incorrect email or password.");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Incorrect email or password.");
            }

            string token = CreateToken(user, request.RememberUser);
            return Ok(token);
        }

        [Authorize]
        [HttpPut("edit")]
        public async Task<ActionResult<User>> Edit(UserEditDto userEdit)
        {
            Console.WriteLine(userEdit);
            var user = await _authRepository.GetByEmail(userEdit.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.FirstName = userEdit.FirstName;
            user.LastName = userEdit.LastName;

            await _authRepository.Update(user);

            string token = CreateToken(user, false);
            return Ok(token);
        }

        private string CreateToken(User user, Boolean rememberUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("name", user.FirstName),
                new Claim("surname", user.LastName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expirationDate = rememberUser ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(20);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expirationDate,
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static bool ValidatePassword(string password)
        {
            // Check for password length (8 to 20 characters)
            if (password.Length < 8 || password.Length > 20)
            {
                return false;
            }

            // Check for at least one uppercase letter
            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                return false;
            }

            // Check for at least one lowercase letter
            if (!Regex.IsMatch(password, "[a-z]"))
            {
                return false;
            }

            // Check for at least one digit
            if (!Regex.IsMatch(password, "[0-9]"))
            {
                return false;
            }

            // Check for at least one special character
            if (!Regex.IsMatch(password, "[!@#$%^&*()-=_+\\[\\]{};':\",./<>?|]"))
            {
                return false;
            }

            // All checks passed, the password is valid
            return true;
        }

        private static bool ValidateEmail(string email)
        {
            // A basic regular expression pattern to validate the email address
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Use Regex.IsMatch to check if the email matches the pattern
            return Regex.IsMatch(email, emailPattern);
        }
    }
}

