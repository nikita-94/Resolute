using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Models;

namespace OnBoarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly OnBoardingContext _context;

        public AgentsController(OnBoardingContext context)
        {
            _context = context;
        }

        // GET: api/Agents
        [HttpGet]
        public IEnumerable<Agent> GetAgent()
        {
            return _context.Agent.Include(x => x.Department).Include(x => x.Organization);
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agent = await _context.Agent.FindAsync(id);

            if (agent == null)
            {
                return NotFound();
            }

            return Ok(agent);
        }


        // POST: api/Agents
        [HttpPost]
        public async Task<IActionResult> PostAgent()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await ExtractData();
        }


        public async Task<IActionResult> ExtractData()
        {
            string filePathCSV = @"D:\Workspace\OnBoarding\OnBoarding\wwwroot\Upload\agent.csv";
            Task<string> fileData = ReadFileAsync(filePathCSV);
            await fileData;
            string[] contents = fileData.Result.Split('\n');


            string[] header = contents[0].Split(',');
            for (int i = 0; i < header.Length; i++)
            {
                header[i] = header[i].Replace("\r",string.Empty).Trim('\"');
            }
            int indexOfName = Array.IndexOf(header, "Name");
            int indexOfEmail = Array.IndexOf(header, "Email");
            int indexOfPhoneNumber = Array.IndexOf(header, "PhoneNumber");
            int indexOfProfileImage = Array.IndexOf(header, "ProfileImg");
            int indexOfDepartment = Array.IndexOf(header, "Department");
            int indexOfOrganizationName = Array.IndexOf(header, "Organization/CustomerName");
            int indexOfOrganizationEmail = Array.IndexOf(header, "Organization/Email");
            int indexOfOrganizationPwd = Array.IndexOf(header, "Organization/Password"D:\OnBoarding\OnBoarding\Properties\);
            int indexOfOrganizationLogo = Array.IndexOf(header, "Organization/LogoUrl");

            for (int i = 1; i <= contents.Count() - 1; i++)
            {
                string[] info = contents[i].Split(',');

                Agent agent = new Agent
                {
                    Name = info[indexOfName].Trim('\"'),
                    Email = info[indexOfEmail].Trim('\"'),
                    Phone_no = info[indexOfPhoneNumber].Trim('\"'),
                    Profile_img_url = info[indexOfProfileImage].Trim('\"'),
                    Department = _context.Department.FirstOrDefault(x=>x.DepartmentName== info[indexOfDepartment].Trim('\"')) ?? new Department { DepartmentName = info[indexOfDepartment].Trim('\"') },
                    Organization = _context.Customer.FirstOrDefault(x => x.Customer_name == info[indexOfOrganizationName].Trim('\"')) ?? new Customer
                    {
                        Customer_name = info[indexOfOrganizationName].Trim('\"'),
                        Email = info[indexOfOrganizationEmail].Trim('\"'),
                        Password = info[indexOfOrganizationPwd].Trim('\"'),
                        Logo_url = info[indexOfOrganizationLogo].Replace("\r", string.Empty).Trim('\"')
                    }
                };
                _context.Agent.Add(agent);
                await _context.SaveChangesAsync();

            }
            return Ok();
        }
        public static async Task<string> ReadFileAsync(string filepath)
        {
            string fileData = "";
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                fileData = await streamReader.ReadToEndAsync();
            }
            return fileData;
        }
    }
}