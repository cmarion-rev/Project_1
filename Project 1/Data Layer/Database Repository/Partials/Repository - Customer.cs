using Data_Layer.Data_Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public partial class Repository
    {
        public async Task<Customer> GetCustomer(int id)
        {
            Customer result = null;

            try
            {
                result = await myContext.Customers.Where(c => c.ID == id).FirstOrDefaultAsync();
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            finally
            {

            }
            return result;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> results = await myContext.Customers.ToListAsync();

            return results;
        }

        public async Task<Customer> UpdateCustomer(Customer currentCustomer)
        {
            myContext.Update(currentCustomer);
            await myContext.SaveChangesAsync();

            return currentCustomer;
        }
    }
}