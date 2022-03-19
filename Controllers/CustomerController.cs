

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using transx.DTOs;
using transx.Models;
using transx.Repositories;
using transx.Services;
using transx.Utilities;

namespace transx.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("customers")]
    public class CustomerController: ControllerBase {
        private readonly ICustomerRepository repository;
        private readonly IUserService userService;
        public CustomerController(ICustomerRepository repository, IUserService userService){
            this.repository = repository;
            this.userService = userService;
        }

        /// GET customers/{username}
        [HttpGet("{username}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(string username){
            try{
                var customer = await this.repository.GetCustomerByLoginName(username);
                if (customer is null){
                    return NotFound();
                }
                return customer.AsDTO();
            }
            catch(Exception){
                return StatusCode (
                    StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database"
                );
            }
        }

        [Route("authenticate/")]
        [HttpPost]
        public async Task<ActionResult<UserAuthenticationResponseDTO>> Authenticate([FromBody] UserAuthenticationRequestDTO user)
        {
            try
            {
                UserAuthenticationResponseDTO customer = await this.userService.Authenticate(user);
                if (customer is null)
                {
                    return  StatusCode (StatusCodes.Status401Unauthorized, "Wrong Credentials");
                }
                
                return Ok(customer);

            }catch(Exception){
                return StatusCode (
                    StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database"
                );
            }
        }
        /// POST customers/{customerDTO}
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CreateCustomerDTO customerDTO){
            try{
                if (customerDTO is null){
                    return BadRequest();
                }
                string password  = PasswordHash.StringHash(customerDTO.Password);
                Customer customer = new Customer(){
                    Id = Guid.NewGuid(),
                    OrganisationOrPerson = customerDTO.OrganisationOrPerson,
                    OrganisationName = customerDTO.Organisation,
                    Gender = customerDTO.Gender,
                    FirstName = customerDTO.FirstName,
                    MiddleName = customerDTO.MiddleName,
                    LastName = customerDTO.LastName,
                    Email = customerDTO.Email,
                    LoginName = customerDTO.Username,
                    LoginPassword = password,
                    PhoneNumber = customerDTO.PhoneNumber,
                    AddressLine1 = customerDTO.PrimaryAddress,
                    AddressLine2 = customerDTO.SecondaryAddress,
                    AddressLine3 = customerDTO.AuxiliaryAddress1,
                    AddressLine4 = customerDTO.AuxiliaryAddress2,
                    TownCity = customerDTO.TownCity,
                    County = customerDTO.County,
                    Country = customerDTO.Country
                };
                // TODO change cust0, cust1 request to a lambda expression
                // check email 
                var cust0 = await this.repository.GetCustomerByEmail(customerDTO.Email);
                if (cust0 is not null){
                    ModelState.AddModelError("Email", "Email already in use by another user");
                    return BadRequest(ModelState);
                }
                // check username
                var cust1 = await this.repository.GetCustomerByLoginName(customerDTO.Username);
                if(cust1 is not null){
                    ModelState.AddModelError("Username", "Username already in use by another user");
                    return BadRequest(ModelState);
                }

                var createdCustomer = await this.repository.CreateCustomer(customer);

                return CreatedAtAction(nameof(GetCustomer), 
                    new {username = createdCustomer.AsDTO().Username}, 
                    createdCustomer.AsDTO()
                );

            }catch(Exception){
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Creating new Customer");
            }
        }

        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateCustomer(string username, UpdatedCustomerDTO customerDTO){
            var customer = await this.repository.GetCustomerByLoginName(username);
            
            if (customer is null){
                return NotFound();
            }

            StringBuilder password = new StringBuilder();
            // update password
            if (customerDTO.NewPassword.Length > 0){
                if (!(customer.LoginPassword == PasswordHash.StringHash(customerDTO.Password))){
                    ModelState.AddModelError("Password", "Wrong Password, verify your password");
                    return BadRequest(ModelState);
                }else{
                    password.Append(PasswordHash.StringHash(customerDTO.NewPassword));
                }
            }else{
                password.Append(customer.LoginPassword);
            }
            
            
            Customer updatedCustomer = new Customer(){
                Id = customer.Id,
                OrganisationName = customerDTO.Organisation,
                OrganisationOrPerson = customerDTO.OrganisationOrPerson,
                Gender = customerDTO.Gender,
                FirstName = customerDTO.FirstName,
                MiddleName = customerDTO.MiddleName,
                LastName = customerDTO.LastName,
                Email = customerDTO.Email,
                LoginName = customerDTO.Username,
                LoginPassword = password.ToString(),
                PhoneNumber = customerDTO.PhoneNumber,
                AddressLine1 = customerDTO.PrimaryAddress,
                AddressLine2 = customerDTO.SecondaryAddress,
                AddressLine3 = customerDTO.AuxiliaryAddress1,
                AddressLine4 = customerDTO.AuxiliaryAddress2,
                TownCity = customerDTO.TownCity,
                County = customerDTO.County,
                Country = customerDTO.Country
            };
            await this.repository.UpdateCustomer(updatedCustomer);

            return NoContent();

        }

        //! Warning: Need to add credentials
        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteCustomer(string username){
            var customer = await this.repository.GetCustomerByLoginName(username);
            if (customer is null){
                return NotFound();
            }
            
            this.repository.DeleteCustomer(customer);

            return NoContent();
        }
    }
}