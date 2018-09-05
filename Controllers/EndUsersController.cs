using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace OnBoarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndUsersController : ControllerBase
    {
        private readonly OnBoardingContext _context;

        public EndUsersController(OnBoardingContext context)
        {
            _context = context;
        }

        // GET: api/EndUsers
        [HttpGet]
        public IEnumerable<EndUser> GetEndUser()
        {
            return _context.EndUser.Include(x=>x.SocialId).Include(x=>x.Organization);
        }

        // GET: api/EndUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEndUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var endUser = await _context.EndUser.FindAsync(id);

            if (endUser == null)
            {
                return NotFound();
            }

            return Ok(endUser);
        }


        // POST: api/EndUsers
        [HttpPost]
        public async Task<IActionResult> PostEndUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await ExtractData();
        }

        public async Task<IActionResult> ExtractData()
        {
            string filePathCSV = @"D:\Project\onboarding\OnBoarding\wwwroot\Upload\EndUser.csv";
            Task<string> fileData = ReadFileAsync(filePathCSV);
            await fileData;
            string[] contents = fileData.Result.Split('\n');
            int countOfSocialIds = Regex.Matches(contents[0], "/Source").Count;
            string[] header = contents[0].Split(',');
            for (int i = 0; i < header.Length; i++)
            {
                header[i] = header[i].Replace("\r", string.Empty).Trim('\"');
            }
            int indexOfName = Array.IndexOf(header, "Name");
            int indexOfEmail = Array.IndexOf(header, "Email");
            int indexOfPhoneNumber = Array.IndexOf(header, "PhoneNumber");
            int indexOfProfileImage = Array.IndexOf(header, "ProfileImgUrl");
            int[] indexOfSocialAccountSource = new int[countOfSocialIds];
            int[] indexOfSocialAccountIdentifier = new int[countOfSocialIds];
            for (int i=0;i<countOfSocialIds;i++)
            {
                indexOfSocialAccountSource[i]= Array.IndexOf(header, $"SocialId/{i}/Source");
                indexOfSocialAccountIdentifier[i] = Array.IndexOf(header, $"SocialId/{i}/Identifier");
            }
            int indexOfOrganizationName = Array.IndexOf(header, "Organization/CustomerName");
            int indexOfOrganizationEmail = Array.IndexOf(header, "Organization/Email");
            int indexOfOrganizationPwd = Array.IndexOf(header, "Organization/Password");
            int indexOfOrganizationLogo = Array.IndexOf(header, "Organization/LogoUrl");

            for (int i = 1; i <= contents.Count() - 1; i++)
            {
                string[] info = contents[i].Split(',');

                EndUser agent = new EndUser
                {
                    Name = info[indexOfName].Trim('\"'),
                    Email = info[indexOfEmail].Trim('\"'),
                    Phone_no = info[indexOfPhoneNumber].Trim('\"'),
                    Profile_img_url = info[indexOfProfileImage].Trim('\"'),
                    SocialId = new List<UserSocialId>(),
                    Organization = _context.Customer.FirstOrDefault(x => x.Customer_name == info[indexOfOrganizationName].Trim('\"')) ?? new Customer
                    {
                        Customer_name = info[indexOfOrganizationName].Trim('\"'),
                        Email = info[indexOfOrganizationEmail].Trim('\"'),
                        Password = info[indexOfOrganizationPwd].Trim('\"'),
                        Logo_url = info[indexOfOrganizationLogo].Replace("\r", string.Empty).Trim('\"')
                    }

                };

                for (int j = 0; j < countOfSocialIds; j++)
                {
                    if (info[indexOfSocialAccountSource[j]].Trim('\"') != string.Empty && info[indexOfSocialAccountIdentifier[j]].Trim('\"') != string.Empty)
                    {
                        agent.SocialId.Add(new UserSocialId { Source = info[indexOfSocialAccountSource[j]].Trim('\"'), Identifier = info[indexOfSocialAccountIdentifier[j]].Trim('\"') });
                    }
                }
        
                _context.EndUser.Add(agent);
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