

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using transx.DataAccess;
using transx.Models;

namespace transx.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShipmentContext shipmentContext;
        public CustomerRepository(ShipmentContext shipmentContext)
        {
            this.shipmentContext = shipmentContext;
        }

        // public async Task<IEnumerable<Customer>> GetCustomers()
        // {
        //     return await shipmentContext.Customers.ToListAsync();
        // }

        public async Task<Customer> GetCustomer(Guid Id)
        {
            return await this.shipmentContext.Customers
                .FirstOrDefaultAsync(cust => cust.Id == Id);
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            var ret = await this.shipmentContext.Customers.AddAsync(customer);
            await this.shipmentContext.SaveChangesAsync();
            return ret.Entity;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var ret = await this.shipmentContext.Customers
                .FirstOrDefaultAsync(cust => cust.Id == customer.Id);

            if (ret is not null)
            {
                ret.copyFrom(customer);
                await this.shipmentContext.SaveChangesAsync();
                return ret;
            }
            // not found return null
            return ret;
        }

        public async void DeleteCustomer(Guid id)
        {
            var ret = await this.shipmentContext.Customers
                .FirstOrDefaultAsync(cust => cust.Id == id);
            if (ret is not null)
            {
                this.shipmentContext.Customers.Remove(ret);
                await this.shipmentContext.SaveChangesAsync();
            }
        }
    }
}