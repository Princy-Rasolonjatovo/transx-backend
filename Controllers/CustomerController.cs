

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using transx.DTOs;
using transx.Models;
using transx.Repositories;
using transx.Utilities;

namespace transx.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController: ControllerBase {
        private readonly ICustomerRepository repository;

        public CustomerController(ICustomerRepository repository){
            this.repository = repository;
        }

        /// GET customers/{username}
        [HttpGet("{username:string}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(string username){
            try{
                var customer = await this.repository.GetCustomer(username);
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
                var createdCustomer = await this.repository.CreateCustomer(customer);

                return CreatedAtAction(nameof(GetCustomer), 
                    new {username = createdCustomer.AsDTO().Username}, 
                    createdCustomer.AsDTO()
                );

            }catch(Exception){
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Creating new Customer");
            }
        }

    }
}