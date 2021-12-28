using System.ComponentModel.DataAnnotations;

namespace transx.DTOs
{
    public record UpdatedCustomerDTO{
            
            // organisation: true; person: false
            public bool OrganisationOrPerson { get; set;} 
            public string? Organisation { get; set;}
            // male: true; female: false
            public bool Gender { get; set;}
            [Required]
            public string FirstName { get; set;}
            public string? MiddleName { get; set;}
            [Required]
            public string LastName { get; set;}
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [StringLength(60, ErrorMessage ="username must be between {2} and {1} characters", MinimumLength = 4)]
            public string Username { get; set;}
            [Required]
            public string Password { get; set;}
            
            public string? NewPassword { get; set;}
            [Required]
            public string PhoneNumber { get; set;}
            [Required]
            [StringLength(255)]
            public string PrimaryAddress { get; set;}
            [StringLength(255)]
            public string? SecondaryAddress { get; set;}
            [StringLength(255)]
            public string? AuxiliaryAddress1 { get; set;}
            [StringLength(255)]
            public string? AuxiliaryAddress2 { get; set;}
            [Required]
            public string TownCity { get; set;}
            [Required]
            public string County { get; set;}
            [Required]
            public string Country { get; set;}
        

    }
}