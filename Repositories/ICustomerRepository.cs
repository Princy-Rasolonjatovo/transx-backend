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
        Task<Customer> GetCustomer(Guid Id);

        Task<Customer> GetCustomer(string LoginName);
        // Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> UpdateCustomer(Customer customer);
    }
}