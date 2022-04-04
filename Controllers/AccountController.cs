using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MVC_2.Data;
using MVC_2.Models;
using Microsoft.AspNetCore.Cors;

namespace MVC_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors]
    public class AccountController : ControllerBase
    {
        private readonly bookContext _context;

        public AccountController(bookContext context)
        {
            _context = context;
           
        }

        // GET: api/Account/Token
        [Route("Token")]
        [HttpPost]
        public IActionResult Token(LoginModel model)
        {
            var identity = GetIdentity(model.Email, model.Password);
            if (identity == null)
            {
                Console.WriteLine("Invalid username or password. " + model.Email + "++" + model.Password);
                return BadRequest(new { errorText = "Invalid username or password. "+ model.Email + " "+ model.Password });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return new JsonResult(response);
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            User person = _context.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
            string role = _context.Roles.FirstOrDefault(r => r.Id == person.RoleId).Name;
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
