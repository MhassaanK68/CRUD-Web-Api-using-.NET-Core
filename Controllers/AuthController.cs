using CRUD_API.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IConfiguration _config;
        public readonly DatabaseContext _db;
        public AuthController(DatabaseContext db, IConfiguration Config)
        {
            _db = db;
            _config = Config;

        }



      [HttpPost("register")]
      public string register(Users request)
      {
            try
            {
            _db.Users.Add(request);
            _db.SaveChanges();
            return "User Account Created";
            }
            catch (Exception ex) 
            {
                return "Database Error Occoured" + ex.Message;  
            }
                       
      }

        [HttpPost("login")]
        public string login(Users request)
        {
            if (_db.Users.Any(Us => Us.Username == request.Username)) 
            {
                Users ThisUser = _db.Users.FirstOrDefault(x => x.Username == request.Username);
                if (ThisUser.Password == request.Password)
                {
                    try
                    {
                        return "Token : " + CreateToken(request);
                    }
                    catch (Exception e)
                    {
                        return "An Error Occured During Token Creation";
                    }
                }
                else
                {
                    return "Inccorect Password Entered";    
                }
            }
            else 
            {
                return "User Not Found"; 
            }
        }

        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            { 
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var Audience = _config["Jwt:Audiance"];
            var Issuer = _config["Jwt:Issuer"];
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                audience: Audience, 
                issuer: Issuer,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: Credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }




    }
}
