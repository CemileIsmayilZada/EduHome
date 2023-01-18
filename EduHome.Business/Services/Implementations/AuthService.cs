using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities.Identity;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<TokenResponseDTO> LoginAsync(LoginDTO login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user is null) throw new AuthCreateFailException("Username or Password are invalid ");

            var check = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!check) throw new AuthCreateFailException("Username or Password are invalid");

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email)

            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWtSettings:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                 issuer: _configuration["JWtSettings:Issues"],
                 audience: _configuration["JWtSettings:Audience"],
                 claims: claims,
                 notBefore: DateTime.UtcNow,
                 expires: DateTime.UtcNow.AddMinutes(10),
                 signingCredentials: signingCredentials
                );


            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new()
            {
                Token = token,
                ExpireDate = jwtSecurityToken.ValidTo,
                UserName= user.UserName,
            };


        }

        public async Task RegisterAsync(RegisterDTO register)
        {
            AppUser user = new AppUser()
            {
                Fullname = register.Fullname,
                Email = register.Email,
                UserName = register.UserName

            };

            var identityUser = await _userManager.CreateAsync(user, register.Password);
            if (!identityUser.Succeeded)
            {
                string errors = string.Empty;
                int count = 0;
                foreach (var error in identityUser.Errors)
                {
                    errors += count != 0 ? $"{error.Description}" : $",{error.Description}";

                }
                throw new UserCreateFailException(errors);

            }
            var result = await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                int count = 0;
                foreach (var error in result.Errors)
                {
                    errors += count != 0 ? $"{error.Description}" : $",{error.Description}";

                }
                throw new RoleCreateFailException(errors);

            }

        }
    }
}
