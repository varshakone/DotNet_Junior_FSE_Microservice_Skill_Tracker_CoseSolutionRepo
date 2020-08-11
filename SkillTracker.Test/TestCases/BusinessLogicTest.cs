using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
using System.Threading;
using System.Threading.Tasks;
using User_Microservice.BusinessLayer.Interface;
using User_Microservice.BusinessLayer.Service.Repository;
using User_Microservice.Entities;
using Xunit;

namespace SkillTracker.Tests.TestCases
{
  
    [Collection("parallel")]
    public  class BusinessLogicTest
    {
        // private references declaration
        
        IConfigurationRoot config;
      
        private readonly ISkillRepository _skillRepository;
        
        private Skill _skill;
        private Skill_MicroService.DataLayer.IMongoDBContext context;
       

        private ISkillService _skillService;
        
        
        static FileUtility fileUtility;
        String testResult;



        public BusinessLogicTest()
        {
           

          

            MongoDBUtility_Skill mongoDBUtilitys = new MongoDBUtility_Skill();
            context = mongoDBUtilitys.MongoDBContext;
            _skillRepository = new SkillRepository(context);

           
            _skillService = new SkillService(_skillRepository);
            _skill = new Skill
            {
                SkillName = "Azure Business",
                SkillCategory = SkillCategory.DotNet,
                SkillLevel = SkillLevel.Intermediate,
                SkillType = SkillType.Programming,
                SkillTotalExperiance = 1
            };

           
            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }
        static BusinessLogicTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_business_revised.txt";
            fileUtility.CreateTextFile();
        }

        /// <summary>
        /// Test methods for Skill Service
        /// Test method to create new skill
        /// </summary>
        /// <returns></returns>
        [Fact]
     public async Task TestFor_AddNewSkill()
        {
            try
            {
          
                
                var result =await _skillService.AddNewSkill(_skill);
                if (result == "New Skill Added")
                {
                    testResult = "TestFor_AddNewSkill=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    
                }
                else
                {
                    Assert.Equal("New Skill Added", result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_AddNewSkill=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                
            }
        }

        /// <summary>
        /// test method to update skill
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_EditSkill()
        {
            try
            {
                fileUtility.TestSkill(_skill);

                _skill.SkillLevel = SkillLevel.Expert;
                _skill.SkillTotalExperiance = 10;
                var result =await  _skillService.EditSkill(_skill);
                if (result == 1)
                {
                    testResult = "TestFor_EditSkill=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    
                }
                else
                {
                    Assert.Equal(1, result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_EditSkill=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
               
            }
        }

        /// <summary>
        /// test method to delete skill
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestFor_DeleteSkill()
        {
            try
            {
              fileUtility.TestSkill(_skill);

                var result =await _skillService.DeleteSkill(_skill.SkillName);
                if (result == 1)
                {
                    testResult = "TestFor_DeleteSkill=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    
                }
                else
                {
                    Assert.Equal(1, result);
                }
            }
            catch (Exception exception)
            {
                var res = exception.Message;
                testResult = "TestFor_DeleteSkill=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                
            }
        }

        
        
    }
}
