using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skill_MicroService.BusinessLayer.Interface;
using Skill_MicroService.Entities;

namespace Skill_MicroService.Controllers
{
    [Route("api/skill")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        //Reference of type ISkillService
        private readonly ISkillService _skillService;


        /// <summary>
        /// Inject SkillService object through constructor
        /// </summary>
        /// <param name="skillService"></param>
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [Route("test")]
        [HttpGet]
        public String test()
        {
            try
            {
                //Business logic to call user servic method which returns success message after creating new user
                return "Hi Users";
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }

        }
        /// <summary>
        /// Rest post api to return success message after creating skill
        /// Post:api/skill/new
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>

        [Route("new")]
        [HttpPost]
        public async Task<ActionResult<String>> NewSkill(Skill skill)
        {
            try
            {
                //Business logic to call user servic method which returns success message after creating new skill
                var result =await _skillService.AddNewSkill(skill);
                return result;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        /// <summary>
        /// Rest post api to return 1 after updation of skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        
        [Route("edit")]
        [HttpPut]
        public async Task<ActionResult<int>> ReviseSkill(Skill skill)
        {
            try
            { 
                //Business logic to call skill servic method which returns 1 on successfull updation of skill
                var result =await _skillService.EditSkill(skill);
                return result;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        /// <summary>
        /// Rest post api to return 1 after deletion of skill
        /// </summary>
        /// <param name="SkillName"></param>
        /// <returns></returns>
        
        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<int>> DestroySkill (String SkillName)
        {
            try
            {
                //Business logic to call skill servic method which returns 1 on successfull deletion of skill
                var result =await _skillService.DeleteSkill(SkillName);
                return result;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }
    }
}