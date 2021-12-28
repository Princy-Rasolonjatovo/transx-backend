using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using transx.Models;

namespace transx.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customer);
        void DeleteCustomer(Guid id);
        Task<Customer> GetCustomerById(Guid Id);

        Task<Customer> GetCustomerByLoginName(string LoginName);
        Task<Customer> GetCustomerByEmail(string email);

        Task<Customer> UpdateCustomer(Customer customer);
    }
}