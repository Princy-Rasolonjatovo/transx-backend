using transx.DTOs;
using transx.Models;

namespace transx
{
    public static class Extensions{
        public static CustomerDTO AsDTO(this Customer customer){
            return new CustomerDTO
            {
                Id = customer.Id,
                OrganisationOrPerson = customer.OrganisationOrPerson,
                Organisation = customer.OrganisationName,
                Gender = customer.Gender,
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName, 
                Email  = customer.Email, 
                Username = customer.LoginName, 
                Password = customer.LoginPassword, 
                PhoneNumber = customer.PhoneNumber, 
                PrimaryAddress = customer.AddressLine1, 
                SecondaryAddress = customer.AddressLine2, 
                AuxiliaryAddress1 = customer.AddressLine3, 
                AuxiliaryAddress2 = customer.AddressLine4, 
                TownCity = customer.TownCity, 
                County = customer.County,
                Country = customer.Country,
            };
        }
    }
}