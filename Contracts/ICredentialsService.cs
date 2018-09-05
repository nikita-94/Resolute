using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoarding.Models;

namespace OnBoarding.Services
{
    public interface ICredentialsService
    {
        Task CreateCredentials(Customer customer_Signup);
        IEnumerable<Customer> GetAllSignUp();
        Task<Customer> GetSignUp(int id);
        Customer GetAllCustomer(string Customer_name, string Email);
    }
}