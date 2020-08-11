
using Skill_MicroService.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skill_MicroService.BusinessLayer.Service.Repository
{
   public interface ISkillRepository
    {
        Task<String> AddNewSkill(Skill skill);
        Task<int> EditSkill(Skill skill);
        Task<int> DeleteSkill(String skillname);
    }
}
