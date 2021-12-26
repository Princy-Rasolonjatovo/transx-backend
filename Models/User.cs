using System;

namespace transx.Models
{
    public record User{
        public Guid Id{get; init;}

        public string name { get; set; }
    }
}