using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Skill_MicroService.Entities;
using User_Microservice.Entities;
using SkillTracker.Tests.Utility;
using User_Microservice.BusinessLayer.Service.Repository;
using User_Microservice.BusinessLayer.Interface;
using SkillTracker.BusinessLayer.Service;
using Skill_MicroService.BusinessLayer.Service.Repository;
using Skill_MicroService.BusinessLayer.Interface;
using Skill_MicroService.BusinessLayer.Service;

namespace SkillTracker.Test.Utility
{
   public  class FileUtility
    {
        private  static FileStream stream;
        private static XmlSerializer serialize;
        private String filePath;
    

      
        private  List<cases> Testcases;

        public string FilePath { get => filePath; set => filePath = value; }

        public FileUtility()
        {
            serialize = new XmlSerializer(typeof(List<cases>));
            this.Testcases = new List<cases>();
        }
        public async Task<string>  WriteTestCaseResuItInXML(cases Cases)
        {
            try { 
            
            if (File.Exists("../../../../testcases.xml"))
            {
                 stream = new FileStream("../../../../testcases.xml", FileMode.Open, FileAccess.Read);
                 
                var testcases =  serialize.Deserialize(stream) as List<cases>;
                if (testcases != null)
                {
                    foreach (cases item in testcases)
                    {
                        Testcases.Add(item);
                    }
                }
                stream.Close();
                Testcases.Add(Cases);
                File.Delete("../../../../testcases.xml");
                stream = new FileStream("../../../../testcases.xml", FileMode.CreateNew, FileAccess.Write);
                serialize.Serialize(stream, Testcases);
                    stream.Close();
            }
            else
            {
                
               stream = new FileStream("../../../../testcases.xml", FileMode.OpenOrCreate, FileAccess.Write);
               Testcases.Add(Cases);
                serialize.Serialize(stream, Testcases);
                stream.Close();

            }
               return "test case registered";
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public  void WriteTestCaseResuItInText(String testresult)
        {
            try
            {
                File.AppendAllText(FilePath,  testresult + "\n");
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        public  void CreateTextFile()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath).Dispose();
                }

                else
                {
                    File.Delete(FilePath);
                    File.Create(FilePath).Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public   void TestData(User user)
        {
            try
            {
                MongoDBUtility_User mongoDBUtility = new MongoDBUtility_User();
                UserRepository userRepository = new UserRepository(mongoDBUtility.MongoDBContext);
                IUserService userService = new UserService(userRepository);

                var result = userRepository.ValidateUserExist(user);
                if (result == "User Exist")
                {
                    var rr = userService.RemoveUser(user.FirstName, user.LastName).Result;

                   
                    var lst = userService.GetAllUsers();
                    if (rr ==1)
                    {
                        result =userService.CreateNewUser(user).Result;
                    }
                  
                }
                else
                {
                     result =  userService.CreateNewUser(user).Result;
                }
               
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void ValidateUser(User user)
        {
            try
            {
                MongoDBUtility_User mongoDBUtility = new MongoDBUtility_User();
                UserRepository userRepository = new UserRepository(mongoDBUtility.MongoDBContext);
                IUserService userService = new UserService(userRepository);
                var result = userRepository.ValidateUserExist(user);
                if(result == "User Exist")
                {
                    userService.RemoveUser(user.FirstName, user.LastName);
                }
               
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void TestSkill(Skill skill)
        {
            try
            {
                MongoDBUtility_Skill mongoDBUtility = new MongoDBUtility_Skill();
                SkillRepository skillRepository = new SkillRepository(mongoDBUtility.MongoDBContext);
                ISkillService skillService = new SkillService(skillRepository);

                var result = skillRepository.SkillExist(skill);
                if (result == "Skill Exist")
                {
                   
                    
                     result = skillRepository.DeleteSkill(skill.SkillName).Result.ToString();
                   var result1 = skillRepository.AddNewSkill(skill).Result;

                }
                else
                {
                    result = skillRepository.AddNewSkill(skill).Result;
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
