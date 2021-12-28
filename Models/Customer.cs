using System;

namespace transx.Models
{
    public class Customer{

        public Guid Id { get; init;}
        // organisation: true; person: false
        public bool OrganisationOrPerson { get; set;} 
        public string? OrganisationName { get; set;}
        // male: true; female: false
        public bool Gender { get; set;}

        public string FirstName { get; set;}
        public string? MiddleName { get; set;}
        public string LastName { get; set;}
        public string Email { get; set; }
        public string LoginName { get; set;}
        public string LoginPassword { get; set;}
        public string PhoneNumber { get; set;}
        public string AddressLine1 { get; set;}
        public string? AddressLine2 { get; set;}
        public string? AddressLine3 { get; set;}
        public string? AddressLine4 { get; set;}
        public string TownCity { get; set;}
        public string County { get; set;}
        public string Country { get; set;}
        public void copyFrom(Customer customer){
            this.OrganisationOrPerson = customer.OrganisationOrPerson;
            this.OrganisationName = customer.OrganisationName;
            this.Gender = customer.Gender;
            this.FirstName = customer.FirstName;
            this.MiddleName = customer.MiddleName;
            this.LastName = customer.LastName; 
            this.Email  = customer.Email; 
            this.LoginName = customer.LoginName; 
            this.LoginPassword = customer.LoginPassword; 
            this.PhoneNumber = customer.PhoneNumber; 
            this.AddressLine1 = customer.AddressLine1; 
            this.AddressLine2 = customer.AddressLine2; 
            this.AddressLine3 = customer.AddressLine3; 
            this.AddressLine4 = customer.AddressLine4; 
            this.TownCity = customer.TownCity; 
            this.County = customer.County; 
            this.Country = customer.Country;
        }

    }
}