using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Areas.Management.Models;
using Portfolio.CustomAuthorization;
using Portfolio.Data.Entities;
using Portfolio.Data.UnitOfWork;

namespace Portfolio.Areas.Management.Controllers
{
    [Area("Management")]
    [CustomAuthorization]
    public class TechnicalSkillsController : BaseController<TechnicalSkillsController>
    {

        public TechnicalSkillsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IActionResult> IndexAsync()
        {
            var technicalSkills = await _unitOfWork.TechnicalSkillRepository.GetAsync();
            var technicalSkillViewModels = _mapper.Map<List<TechnicalSkillViewModel>>(technicalSkills);
            technicalSkillViewModels.Reverse();
            return View(technicalSkillViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] TechnicalSkillViewModel technicalSkillViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            TechnicalSkill technicalSkill = _mapper.Map<TechnicalSkill>(technicalSkillViewModel);
            await _unitOfWork.TechnicalSkillRepository.AddAsync(technicalSkill);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync([FromForm] TechnicalSkillViewModel technicalSkillViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            TechnicalSkill technicalSkill = _mapper.Map<TechnicalSkill>(technicalSkillViewModel);
            _unitOfWork.TechnicalSkillRepository.Update(technicalSkill);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var technicalSkill = await _unitOfWork.TechnicalSkillRepository.GetFirstByFilter(t => t.Id == id);
            _unitOfWork.TechnicalSkillRepository.Delete(technicalSkill);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }

    }
}