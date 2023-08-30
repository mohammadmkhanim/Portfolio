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
    public class ProjectsController : BaseController<ProjectsController>
    {

        public ProjectsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IActionResult> IndexAsync()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAsync(includeProperties: new List<string>() { "Image" });
            var projectViewModels = _mapper.Map<List<ProjectViewModel>>(projects);
            projectViewModels.Reverse();
            return View(projectViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Project project = _mapper.Map<Project>(projectViewModel);
            string uniqueImageName = SaveImageAndGetTheImageName(projectViewModel.ImageFile);
            Image projectImage = new Image()
            {
                Name = uniqueImageName,
                Title = project.Title,
                Alt = project.Title + " image"
            };
            project.Image = projectImage;
            await _unitOfWork.ProjectRepository.AddAsync(project);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync([FromForm] EditProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Project project = _mapper.Map<Project>(projectViewModel);

            if (projectViewModel.ImageFile != null)
            {
                var image = await _unitOfWork.ImageRepository.GetByIdAsync(projectViewModel.ImageId);
                DeleteImage(image.Name);
                _unitOfWork.ImageRepository.Delete(image);
                string uniqueImageName = SaveImageAndGetTheImageName(projectViewModel.ImageFile);
                Image projectImage = new Image()
                {
                    Name = uniqueImageName,
                    Title = project.Title,
                    Alt = project.Title + " image"
                };
                project.Image = projectImage;
            }
            _unitOfWork.ProjectRepository.Update(project);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetFirstByFilter(s => s.Id == id, includeProperties: new List<string>() { "Image" });
            DeleteImage(project.Image.Name);
            _unitOfWork.ProjectRepository.Delete(project);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }
    }
}