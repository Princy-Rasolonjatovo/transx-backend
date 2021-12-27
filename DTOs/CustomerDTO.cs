using System;

namespace transx.DTOs
{
     public record Customer{

        public Guid Id { get; init;}
        // organisation: true; person: false
        public bool OrganisationOrPerson { get; set;} 
        public string Organisation { get; set;}
        // male: true; female: false
        public bool Gender { get; set;}

        public string FirstName { get; set;}
        public string MiddleName { get; set;}
        public string LastName { get; set;}
        public string Email { get; set; }
        public string LoginName { get; set;}
        public string Password { get; set;}
        public string PhoneNumber { get; set;}
        public string AddressLine1 { get; set;}
        public string AddressLine2 { get; set;}
        public string AddressLine3 { get; set;}
        public string AddressLine4 { get; set;}
        public string TownCity { get; set;}
        public string County { get; set;}
        public string Country { get; set;}
        

    }
}