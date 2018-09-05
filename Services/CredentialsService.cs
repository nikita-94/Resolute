using OnBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnBoarding.Services
{
    public class CredentialsService : ICredentialsService
    {
        private readonly OnBoardingContext _context;

        public CredentialsService(OnBoardingContext context)
        {
            _context = context;
        }
        public IEnumerable<Customer> GetAllSignUp()
        {
            return _context.Customer;
        }
        public async Task<Customer> GetSignUp(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        public async Task CreateCredentials(Customer customer_Signup)
        {
            _context.Customer.Add(customer_Signup);
            await _context.SaveChangesAsync();
        }
        public Customer GetAllCustomer(string Customer_name, string Email)
        {
            return _context.Customer.Where(

                element => element.Customer_name == Customer_name
                || element.Email == Email
                ).ToList()[0];
        }
    }
}
