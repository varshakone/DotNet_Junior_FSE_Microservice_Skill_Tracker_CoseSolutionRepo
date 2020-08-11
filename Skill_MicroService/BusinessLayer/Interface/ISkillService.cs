
using Skill_MicroService.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skill_MicroService.BusinessLayer.Interface
{
   public interface ISkillService
    {
        Task<String> AddNewSkill(Skill skill);
        Task<int> EditSkill(Skill skill);
        Task<int> DeleteSkill(String skillname);

    }
}
