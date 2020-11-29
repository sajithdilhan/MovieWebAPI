using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieWebAPI.Data;
using MovieWebAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebAPI.Services
{
    public class UserService : IUserService
    {
        public IConfiguration Configuration { get; }

        private readonly MovieAPIContext dbContext;

        public UserService(IConfiguration configuration, MovieAPIContext _dbContext)
        {
            Configuration = configuration;
            dbContext = _dbContext;
        }

        public string Login(string userName, string password)
        {
            var user = dbContext.User.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            // return null if user not found
            if (user == null)
            {
                return string.Empty;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["SECRETKEY"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }
    }
}
