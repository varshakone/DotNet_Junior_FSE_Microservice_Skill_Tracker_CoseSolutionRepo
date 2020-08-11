using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using Skill_MicroService.BusinessLayer.Interface;
using Skill_MicroService.BusinessLayer.Service;
using Skill_MicroService.BusinessLayer.Service.Repository;
using Skill_MicroService.DataLayer;
using Skill_MicroService.Entities;
using SkillTracker.BusinessLayer.Service;
using SkillTracker.Test.Utility;
using SkillTracker.Tests.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using User_Microservice.BusinessLayer.Interface;
using User_Microservice.BusinessLayer.Service.Repository;
using User_Microservice.Entities;
using Xunit;

namespace SkillTracker.Tests.TestCases
{

    [CollectionDefinition("parallel", DisableParallelization = false)]
    public   class BoundaryTest
    {
        // private references
        private User _user;
        IConfigurationRoot config;
        private Skill _skill;
       
        private ISkillService _skillService;
        private IUserService _userService;
        
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;

        private Skill_MicroService.DataLayer.IMongoDBContext context;
        private User_Microservice.DataLayer.IMongoDBContext ucontext;
        static FileUtility fileUtility;
        String testResult;

        /// <summary>
        /// 
        /// </summary>
        public BoundaryTest()
        {
         _skill = new Skill
            {
                SkillName = ".Net core 3.1",
                SkillCategory = SkillCategory.DotNet,
                SkillLevel = SkillLevel.Intermediate,
                SkillType = SkillType.Programming,
                SkillTotalExperiance = 1
            };

            _user = new User
            {

                FirstName = "Dnyati",
                LastName = "Dube",
                Email = "dnyati@gmail.com",
                Mobile = 9685744263,
            };

            MongoDBUtility_User mongoDBUtility = new MongoDBUtility_User();
            ucontext = mongoDBUtility.MongoDBContext;

            _userRepository = new UserRepository(ucontext);

            MongoDBUtility_Skill mongoDBUtilitys = new MongoDBUtility_Skill();
            context = mongoDBUtilitys.MongoDBContext;
            _skillRepository = new SkillRepository(context);

            _userService = new UserService(_userRepository);
            _skillService = new SkillService(_skillRepository);
            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }

        /// <summary>
        /// Static constructor to create text file to write test result
        /// </summary>
        static BoundaryTest()
        {
            fileUtility = new FileUtility
            {
                FilePath = "../../../../output_boundary_revised.txt"
            };
            fileUtility.CreateTextFile();
        }



        // Test methods for User Service
        /// <summary>
        /// Validate Email Id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_ValidEmail()
        {
            try
            {
                bool isEmail = false;
                //_userRepository.Setup(repo => repo.CreateNewUser(_user)).ReturnsAsync(_user);
              
                if (_user.Email != "")
                {
                    Regex regex = new Regex(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$");
                     isEmail = regex.IsMatch(_user.Email);
                    if (isEmail == true)
                    {
                        var result =  _userService.CreateNewUser(_user);
                        testResult = "TestFor_ValidEmail=" + "True";
                        fileUtility.WriteTestCaseResuItInText(testResult);
                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "TestFor_ValidEmail",
                                expectedOutput = "True",
                                weight = 5,
                                mandatory = "True",
                                desc = "expecting to create new user after validating email Id"
                            };
                            await fileUtility.WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    Assert.True(isEmail);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_ValidEmail=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "TestFor_ValidEmail",
                        expectedOutput = "False",
                        weight = 1,
                        mandatory = "False",
                        desc = "expecting to create new user after validating email Id but fail"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// Validate Mobile number length
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_ValidMobileNumberLength()
        {
            try
            {
                bool isMobile = false;
             if (_user.Mobile != 0)
                {
                    if (_user.Mobile.ToString().Length == 10)
                    {
                        isMobile = true ;
                     }
                    if (isMobile == true)
                    {
                        var result = _userService.CreateNewUser(_user);
                        testResult = "TestFor_ValidMobileNumberLength=" + "True";
                        fileUtility.WriteTestCaseResuItInText(testResult);
                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "TestFor_ValidMobileNumberLength",
                                expectedOutput = "True",
                                weight = 5,
                                mandatory = "True",
                                desc = "na"
                            };
                            await fileUtility.WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    Assert.True(isMobile);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_ValidMobileNumberLength=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "TestFor_ValidMobileNumberLength",
                        expectedOutput = "False",
                        weight = 1,
                        mandatory = "False",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }


        /// <summary>
        /// Validate First name and last name 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_ValidFirstAndLastName()
        {
            try
            {
                bool isFirstNameValid = true;
                bool isLastNameValid = true;
                
                if (_user.FirstName != "" && _user.LastName != "")
                {
                    long f;
                    long l;
                    isFirstNameValid = long.TryParse(_user.FirstName, out f);
                    isLastNameValid = long.TryParse(_user.LastName, out l);
                    if (isFirstNameValid == false && isLastNameValid == false)
                    {
                         var result = _userService.CreateNewUser(_user);
                        testResult = "TestFor_ValidFirstAndLastName=" + "True";
                        fileUtility.WriteTestCaseResuItInText(testResult);
                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "TestFor_ValidFirstAndLastName",
                                expectedOutput = "True",
                                weight = 5,
                                mandatory = "True",
                                desc = "expecting to create new user after validating firstname and lastname as non-numeric only"
                            };
                            await fileUtility.WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    Assert.False(isFirstNameValid);
                    Assert.False(isLastNameValid);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_ValidFirstAndLastName=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        expectedOutput = "False",
                        Name = "TestFor_ValidFirstAndLastName",
                        weight = 1,
                        mandatory = "False",
                        desc = "expecting to create new user after validating firstname and lastname as non-numeric only but fail"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }


        //Test Methods for Skill Service
        [Fact]
        public async Task TestFor_ValidSkillName()
        {
            try
            {
                bool isSkillNameValid = true;
             
                
                if (_skill.SkillName != "" )
                {
                    long f;
                  isSkillNameValid = long.TryParse(_skill.SkillName, out f);
                  
                    if (isSkillNameValid == false )
                    {
                        var result = _skillService.AddNewSkill(_skill);
                        testResult = "TestFor_ValidSkillName=" + "True";
                        fileUtility.WriteTestCaseResuItInText(testResult);
                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "TestFor_ValidSkillName",
                                expectedOutput = "True",
                                weight = 5,
                                mandatory = "True",
                                desc = "na"
                            };
                            await fileUtility.WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    Assert.False(isSkillNameValid);
                    
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_ValidSkillName=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "TestFor_ValidSkillName",
                        expectedOutput = "False",
                        weight = 1,
                        mandatory = "False",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }


    }


}
