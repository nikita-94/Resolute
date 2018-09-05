using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnBoarding.Models;

namespace OnBoarding.Services
{
    public interface IAgentService
    {
        Task<string> ReadFileAsync(string filepath);
        Task ExtractData();
        IEnumerable<Agent> GetAgent();
        Task<Agent> GetAgent(int id);
        Task PostAgent();

    }
}

