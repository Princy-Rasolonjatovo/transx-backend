

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using transx.Models;
using transx.Repositories;

namespace transx.Controllers
{
    [ApiController]
    [Route("Customers")]
    public class CustomerController: ControllerBase {
        private readonly ICustomerRepository repository;

        public CustomerController(ICustomerRepository repository){
            this.repository = repository;
        }

        [HttpGet("{Id:Guid}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid Id){
            try{
                var customer = await this.repository.GetCustomer(Id);
                if (customer is null){
                    return NotFound();
                }
                return customer;
            }
            catch(Exception){
                return StatusCode (
                    StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database"
                );
            }
            
            
        }
    }
}