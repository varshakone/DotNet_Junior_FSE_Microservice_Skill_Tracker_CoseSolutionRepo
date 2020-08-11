using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
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
using System.Threading.Tasks;
using User_Microservice.BusinessLayer.Service.Repository;
using User_Microservice.Entities;
using User_Microservice.DataLayer;
using Xunit;

namespace SkillTracker.Tests.TestCases
{
    [CollectionDefinition("parallel", DisableParallelization = false)]
    public  class ExceptionTest
    {
        // private references declaration
        private User _user;
        IConfigurationRoot config;
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;
        private Skill _skill;
        private Skill_MicroService.DataLayer.IMongoDBContext context;
        private User_Microservice.DataLayer.IMongoDBContext ucontext;

        private SkillService _skillService;
        private UserService _userService;
        
        static FileUtility fileUtility;
        String testResult;


        public ExceptionTest()
        {
            MongoDBUtility_User mongoDBUtility = new MongoDBUtility_User();
            ucontext = mongoDBUtility.MongoDBContext;

            _userRepository = new UserRepository(ucontext);

            MongoDBUtility_Skill mongoDBUtilitys = new MongoDBUtility_Skill();
            context = mongoDBUtilitys.MongoDBContext;
            _skillRepository = new SkillRepository(context);

            _userService = new UserService(_userRepository);
            _skillService = new SkillService(_skillRepository);


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

            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }
        static ExceptionTest()
        {
            fileUtility = new FileUtility
            {
                FilePath = "../../../../output_exception_revised.txt"
            };
            fileUtility.CreateTextFile();
        }






        /// <summary>
        /// Test methods for Skill Service
        /// Test method to create new skill,expecting to throw exception
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_AddNewSkill_Fail()
        {
            try
            {
                _skill = null;

                
                var result =await _skillService.AddNewSkill(_skill);
                if (result == "New Skill Added")
                {
                    testResult = "TestFor_AddNewSkill_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_AddNewSkill_Fail",
                            expectedOutput = "False",
                            weight = 5,
                            mandatory = "False",
                            desc = "na"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotEqual("New Skill Added", result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_AddNewSkill_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_AddNewSkill_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// test method to update skill
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_EditSkill_Fail()
        {
            try
            {
                _skill = null;
                
                var result =await _skillService.EditSkill(_skill);
                if (result == 1)
                {
                    testResult = "TestFor_EditSkill_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_EditSkill_Fail",
                            expectedOutput = "False",
                            weight = 5,
                            mandatory = "False",
                            desc = "na"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotEqual(1, result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_EditSkill_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_EditSkill_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }


        /// <summary>
        /// test method to delete skill
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_DeleteSkill_Fail()
        {
            try
            {
                _skill = null;
                
                var result =await _skillService.DeleteSkill(_skill.SkillName);
                if (result == 1)
                {
                    testResult = "TestFor_DeleteSkill_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_DeleteSkill_Fail",
                            expectedOutput = "False",
                            weight = 5,
                            mandatory = "False",
                            desc = "na"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotEqual(1, result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_DeleteSkill_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_DeleteSkill_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// Test Methods for User Service
        /// test method to create new user
        ///</summary>
        /// <returns></returns>

        [Fact]
        public async Task TestFor_CreateNewUser_Fail()
        {
            try
            {
                _user = null;
                
                var result =await  _userService.CreateNewUser(_user);
                if (result == "New User Register")
                {
                    testResult = "TestFor_CreateNewUser_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_CreateNewUser_Fail",
                            expectedOutput = "False",
                            weight = 5,
                            mandatory = "False",
                            desc = "allows to create new user and return success message"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotEqual("New User Register", result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_CreateNewUser_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_CreateNewUser_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
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
        public async Task TestFor_UpdateUser_Fail()
        {
            try
            {
                _user = null;
               
                var result =await _userService.UpdateUser(_user);
                if (result == 1)
                {
                    testResult = "TestFor_UpdateUser_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_UpdateUser_Fail",
                            expectedOutput = "False",
                            weight = 5,
                            mandatory = "False",
                            desc = "na"
                        };
                         await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotEqual(1, result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_UpdateUser_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_UpdateUser_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
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
        public async Task TestFor_RemoveUser_Fail()
        {
            try
            {
                _user = null;
                
                var result =await _userService.RemoveUser(_user.FirstName, _user.LastName);
                if (result == 1)
                {
                    testResult = "TestFor_RemoveUser_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_RemoveUser_Fail",
                            expectedOutput = "False",
                            weight = 5,
                            mandatory = "False",
                            desc = "na"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotEqual(1, result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_RemoveUser_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_RemoveUser_Fail",
                        expectedOutput = "True",
                        weight = 1,
                        mandatory = "True",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// test method to retrieve all users
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_AllUsers_Fail()
        {
            try
            {
                
                var result =await _userService.GetAllUsers() as List<User>;
                if (result != null)
                {
                    testResult = "TestFor_AllUsers_Fail=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_AllUsers_Fail",
                            expectedOutput = "True",
                            weight = 1,
                            mandatory = "True",
                            desc = "na"
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.InRange(result.Count, 1, 100);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_AllUsers_Fail=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_AllUsers_Fail",
                        expectedOutput = "False",
                        weight = 5,
                        mandatory = "False",
                        desc = "expecting  to shows list of users to admin user but fail"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// test method to search user by first name
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_SearchUserByFirstName_Fail()
        {
            try
            {
               
                
                _user.FirstName = null;
                var result =await  _userService.SearchUserByFirstName(_user.FirstName);
                if (result != null)
                {
                    testResult = "TestFor_SearchUserByFirstName_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_SearchUserByFirstName_Fail",
                            expectedOutput = "False",
                            weight = 1,
                            mandatory = "False",
                            desc = "na "
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotEqual(_user.FirstName, result.FirstName);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_SearchUserByFirstName_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_SearchUserByFirstName_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// test method to search user by email id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_SearchUserByEmail_Fail()
        {
            try
            {
                _user.Email = null;
                
                var result =await  _userService.SearchUserByEmail(_user.Email);
                if (result != null)
                {
                    testResult = "TestFor_SearchUserByEmail_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_SearchUserByEmail_Fail",
                            expectedOutput = "False",
                            weight = 1,
                            mandatory = "False",
                            desc = "na "
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotNull(result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_SearchUserByEmail_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_SearchUserByEmail_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// test method to search user by mobile number
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_SearchUserByMobileNumber_Fail()
        {
            try
            {
                _user.Mobile = 0;
                
                var result =await  _userService.SearchUserByMobile(_user.Mobile);
                if (result != null)
                {
                    testResult = "TestFor_SearchUserByMobileNumber_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_SearchUserByMobileNumber_Fail",
                            expectedOutput = "False",
                            weight = 1,
                            mandatory = "False",
                            desc = "na "
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotNull(result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_SearchUserByMobileNumber_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_SearchUserByMobileNumber_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// test method to search user by it's skill range
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_SearchUserBySkillRange_Fail()
        {
            try
            {
               
                
                var result =await  _userService.SearchUserBySkillRange(1,1) as List<User>;
                if (result.Count != 0)
                {
                    testResult = "TestFor_SearchUserBySkillRange_Fail=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "TestFor_SearchUserBySkillRange_Fail",
                            expectedOutput = "False",
                            weight = 1,
                            mandatory = "False",
                            desc = "na "
                        };
                        await fileUtility.WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.NotEmpty(result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_SearchUserBySkillRange_Fail=" + "True";
                fileUtility.WriteTestCaseResuItInText(testResult);
                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "TestFor_SearchUserBySkillRange_Fail",
                        expectedOutput = "True",
                        weight = 5,
                        mandatory = "True",
                        desc = "na"
                    };
                    await fileUtility.WriteTestCaseResuItInXML(newcase);
                }
            }
        }
    }
}
