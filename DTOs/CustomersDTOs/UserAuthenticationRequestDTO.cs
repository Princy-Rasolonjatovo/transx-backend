using System;

namespace transx.DTOs
{
    public record UserAuthenticationRequestDTO
    {
        public string UsernameOrEmail{get; set;}
        public string Password{ get; set;}

    }
}