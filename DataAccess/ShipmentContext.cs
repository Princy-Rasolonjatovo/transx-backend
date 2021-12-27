

using Microsoft.EntityFrameworkCore;
using transx.Models;



namespace transx.DataAccess
{
    public class ShipmentContext: DbContext{
        public ShipmentContext(DbContextOptions options): base(options){
            
        }   

        public DbSet<Customer> Customers{ get; set;}

    }
}