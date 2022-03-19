using System;

namespace transx.DTOs
{
    public record UserAuthenticationResponseDTO
    {
        public Guid Id{get; init;}
        public string Username {get; init;}
        public string Token {get; init;}
    }
}
