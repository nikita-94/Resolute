using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnBoarding.Models;

namespace OnBoarding.Services
{
    public interface IUserService
    {
        //Agent Services

        Task<string> ReadFileAsync(string filepath);
        Task ExtractData(Customer customer);
        IEnumerable<Agent> GetAgent();
        Task<Agent> GetAgent(int id);
        Task PostAgent(Customer customer);
        Agent GetAllAgents(string Name, string Email,string Phone_no);

        //EndUser Services

        IEnumerable<EndUser> GetEndUser();
        Task<EndUser> GetEndUser(int id);
        Task PostEndUser(Customer customer);
        Task ExtractDataEndUser(Customer customer);
        Task<string> ReadFileEndUserAsync(string filepath);
        EndUser GetAllEndUser(string Name, string Email, string Phone_no);
    }
}

