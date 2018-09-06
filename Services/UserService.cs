using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Models;
using System.Text.RegularExpressions;

namespace OnBoarding.Services
{
    public class UserService: IUserService
    {
        private readonly OnBoardingContext _context;

        public UserService(OnBoardingContext context)
        {
            _context = context;
        }
        //Agent Services
        public IEnumerable<Agent> GetAgent()
        {
            return _context.Agent.Include(x => x.Department).Include(x => x.Organization);
        }

        public async Task<Agent> GetAgent( int id)
        {
            return await _context.Agent.FindAsync(id);
        }
        public Agent GetAllAgents(string Name, string Email,string Phone_no)
        {

            string email = Email.Trim('\"').Trim('\\');
            return _context.Agent.Where(
              element => element.Name == Name
             || element.Email == email
             || element.Phone_no == Phone_no
              ).Include(x => x.Department).ToList()[0];



        }
        public async Task PostAgent([FromBody] Customer customer)
        {
            await ExtractData(customer);
        }


        public async Task ExtractData(Customer customer)
        {
            string filePathCSV = @"./wwwroot/Upload/agent.csv";
            Task<string> fileData = ReadFileAsync(filePathCSV);
            await fileData;
            string[] contents = fileData.Result.Split('\n');


            string[] header = contents[0].Split(',');
            for (int i = 0; i < header.Length; i++)
            {
                header[i] = header[i].Replace("\r", string.Empty).Trim('\"');
            }
            int indexOfName = Array.IndexOf(header, "Name");
            int indexOfEmail = Array.IndexOf(header, "Email");
            int indexOfPhoneNumber = Array.IndexOf(header, "PhoneNumber");
            int indexOfProfileImage = Array.IndexOf(header, "ProfileImg");
            int indexOfDepartment = Array.IndexOf(header, "Department");

            for (int i = 1; i <= contents.Count() - 1; i++)
            {
                string[] info = contents[i].Split(',');

                Agent agent = new Agent
                {
                    Name = info[indexOfName].Trim('\"'),
                    Email = info[indexOfEmail].Trim('\"'),
                    Phone_no = info[indexOfPhoneNumber].Trim('\"'),
                    Profile_img_url = info[indexOfProfileImage].Trim('\"'),
                    Department = _context.Department.FirstOrDefault(x => x.DepartmentName == info[indexOfDepartment].Trim('\"')) ?? new Department { DepartmentName = info[indexOfDepartment].Trim('\"'), CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now },
                    Organization = _context.Customer.FirstOrDefault(x => x.Customer_name == customer.Customer_name) ?? customer,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };
                _context.Agent.Add(agent);
                await _context.SaveChangesAsync();
            }
             
        }
        public  async Task<string> ReadFileAsync(string filepath)
        {
            string fileData = "";
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                fileData = await streamReader.ReadToEndAsync();
            }
            return fileData;
        }

        //EndUser Services
        public IEnumerable<EndUser> GetEndUser()
        {
            return _context.EndUser.Include(x => x.SocialId).Include(x => x.Organization);
        }
        
        // GET: api/EndUsers/5
        
        public async Task<EndUser> GetEndUser(int id)
        {
           return await _context.EndUser.Include(x => x.SocialId).Include(x => x.Organization).FirstOrDefaultAsync(x=>x.Id==id);
        }
        
        // POST: api/EndUsers
        public async Task PostEndUser([FromBody] Customer customer)
        {
            await ExtractDataEndUser(customer);
        }
        public EndUser GetAllEndUser(string Name, string Email, string Phone_no)
        {
            string email = Email.Trim('\"').Trim('\\');
            //string phone_No = Phone_no.Trim('\"');
            //string name = Name.Trim('\"');

            return _context.EndUser.Where(
                element => element.Name == Name
               || element.Email == email
               || element.Phone_no == Phone_no
                ).Include(x => x.SocialId).ToList()[0];
        }
        public async Task ExtractDataEndUser(Customer customer)
        {
            string filePathCSV = @"./wwwroot/Upload/EndUser.csv";
            Task<string> fileData = ReadFileEndUserAsync(filePathCSV);
            await fileData;
            string[] contents = fileData.Result.Split('\n');
            int countOfSocialIds = Regex.Matches(contents[0], " / Source").Count;
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
            for (int i = 0; i < countOfSocialIds; i++)
            {
                indexOfSocialAccountSource[i] = Array.IndexOf(header, $"SocialId/{i}/Source");
                indexOfSocialAccountIdentifier[i] = Array.IndexOf(header, $"SocialId/{i}/Identifier");
            }

            for (int i = 1; i <= contents.Count() - 1; i++)
            {
                string[] info = contents[i].Split(',');
                EndUser endUser = new EndUser
                {
                    Name = info[indexOfName].Trim('\"'),
                    Email = info[indexOfEmail].Trim('\"'),
                    Phone_no = info[indexOfPhoneNumber].Trim('\"'),
                    Profile_img_url = info[indexOfProfileImage].Trim('\"'),
                    SocialId = new List<UserSocialId>(),
                    Organization = _context.Customer.FirstOrDefault(x => x.Customer_name == customer.Customer_name) ?? customer,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };
                for (int j = 0; j < countOfSocialIds; j++)
                {
                    if (info[indexOfSocialAccountSource[j]].Trim('\"') != string.Empty && info[indexOfSocialAccountIdentifier[j]].Trim('\"') != string.Empty)
                    {
                        endUser.SocialId.Add(new UserSocialId { Source = info[indexOfSocialAccountSource[j]].Trim('\"'), Identifier = info[indexOfSocialAccountIdentifier[j]].Trim('\"'),
                            CreatedOn = DateTime.Now,UpdatedOn = DateTime.Now
                        });
                    }
                }
                _context.EndUser.Add(endUser);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<string> ReadFileEndUserAsync(string filepath)
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