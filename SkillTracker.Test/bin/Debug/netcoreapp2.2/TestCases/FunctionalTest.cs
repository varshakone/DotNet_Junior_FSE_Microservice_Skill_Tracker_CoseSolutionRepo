using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
//using Skill_MicroService;
//using User_Microservice;
using SkillTrackerApi_Gateway;
using Skill_MicroService.Entities;
using SkillTracker.Test.Utility;
using SkillTracker.Tests.Utility;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using User_Microservice.Entities;
using Xunit;
using User_Microservice.BusinessLayer.Service.Repository;
using User_Microservice.BusinessLayer.Interface;
using SkillTracker.BusinessLayer.Service;

namespace SkillTracker.Tests.TestCases
{
    [CollectionDefinition("parallel", DisableParallelization = false)]
    public class FunctionalTest
    {
        // private references declaration
        private User _user;
        IConfigurationRoot config;
        private readonly IUserRepository _userRepository;
      
  
     
        private User_Microservice.DataLayer.IMongoDBContext ucontext;

       
        private IUserService _userService;

        static FileUtility fileUtility;
        String testResult;



        public FunctionalTest()
        {
            MongoDBUtility_User mongoDBUtility = new MongoDBUtility_User();
            ucontext = mongoDBUtility.MongoDBContext;

            _userRepository = new UserRepository(ucontext);

                     

            _userService = new UserService(_userRepository);
            

            _user = new User
            {

                FirstName = "xyz",
                LastName = "mno",
                Email = "xyz.mno@gmail.com",
                Mobile = 1236548978,
                MapSkills = 2
            };
            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }
        static FunctionalTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_revised.txt";
            fileUtility.CreateTextFile();
        }



        /// <summary>
        /// Test Methods for User Service
        /// test method to create new user
        ///</summary>
        /// <returns></returns>

        [Fact]
        public async Task TestFor_CreateNewUser()
        {
            try
            {
                fileUtility.ValidateUser(_user);
                var result = await _userService.CreateNewUser(_user);
                if (result == "New User Register")
                {
                    testResult = "TestFor_CreateNewUser=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "TestFor_CreateNewUser",
                            expectedOutput = "True",
                            weight = 5,
                            mandatory = "True",
                            desc = "na"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.Equal("New User Register", result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_CreateNewUser=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "TestFor_CreateNewUser",
                        expectedOutput = "False",
                        weight = 5,
                        mandatory = "False",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        /// <summary>
        /// test method to update user
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_UpdateUser()
        {
            try
            {
                User user = new User
                {
                    FirstName = "shashank",
                    LastName = "mishra",
                    Email = "shashank@gmail.com",
                    Mobile = 9960814103,
                    MapSkills = 3

                };
                fileUtility.TestData(user);

                user.Email = "Shashank.mishra@gmail.com";
                user.Mobile = 8874569874;
                user.MapSkills = 5;



                var result = await _userService.UpdateUser(user);
                if (result == 1)
                {
                    testResult = "TestFor_UpdateUser=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "TestFor_UpdateUser",
                            expectedOutput = "True",
                            weight = 5,
                            mandatory = "True",
                            desc = "na"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                    fileUtility.ValidateUser(user);
                }
                else
                {
                    Assert.Equal(1, result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_UpdateUser=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "TestFor_UpdateUser",
                        expectedOutput = "False",
                        weight = 5,
                        mandatory = "False",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
                
            }
        }

        /// <summary>
        /// test method to delete user
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_RemoveUser()
        {
            try
            {
                User user = new User
                {
                    FirstName = "Anay",
                    LastName = "patil",
                    Email = "anay@gmail.com",
                    Mobile = 9968587456,
                    MapSkills = 3

                };
                fileUtility.TestData(user);


                var result = await _userService.RemoveUser(user.FirstName, user.LastName);
                if (result == 1)
                {
                    testResult = "TestFor_RemoveUser=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "TestFor_RemoveUser",
                            expectedOutput = "True",
                            weight = 5,
                            mandatory = "True",
                            desc = "na"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }

                }
                else
                {
                    Assert.Equal(1, result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_RemoveUser=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "TestFor_RemoveUser",
                        expectedOutput = "False",
                        weight = 5,
                        mandatory = "False",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        
    }

}
