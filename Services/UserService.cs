
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using transx.DTOs;
using transx.Helpers;
using transx.Models;
using transx.Repositories;
using System;
using transx.Utilities;

// Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor

namespace transx.Services
{
    public interface IUserService
    {
        Task<UserAuthenticationResponseDTO> Authenticate(UserAuthenticationRequestDTO model);
        Task<Customer> GetUserById(Guid id);
    }

    public class UserService : IUserService
    {
        private ICustomerRepository CustomerRepository;
        private readonly AppSettings _AppSettings;
        public UserService(ICustomerRepository customerRepository, IOptions<AppSettings> appSettings)
        {
            this.CustomerRepository = customerRepository;
            this._AppSettings = appSettings.Value;
        }
        
        public async Task<UserAuthenticationResponseDTO> Authenticate(UserAuthenticationRequestDTO model)
        {
            Customer user = await this.CustomerRepository.GetCustomerByEmail(model.UsernameOrEmail) ?? await this.CustomerRepository.GetCustomerByLoginName(model.UsernameOrEmail); 
            if(user is null || !user.LoginPassword.Equals(PasswordHash.StringHash(model.Password))) return null;
            var token = this.GenerateJWT(user); // generate token

            return user.AsUserAuthenticationResponseDTO(token);
        }

        private string GenerateJWT(Customer user)
        {
            const int tokenDayValid = 7;
            var tokenHandler = new JwtSecurityTokenHandler();           
            var key = Encoding.ASCII.GetBytes(this._AppSettings.Secret);
            var claims = new[]{ new Claim("id", user.Id.ToString())}; // claim based on Id
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims, 
                expires: DateTime.Now.AddDays(tokenDayValid), 
                signingCredentials: credentials
            );
            return tokenHandler.WriteToken(token);
        }
        public Task<Customer> GetUserById(Guid id)
        {
            return this.CustomerRepository.GetCustomerById(id);
        }
    }
}