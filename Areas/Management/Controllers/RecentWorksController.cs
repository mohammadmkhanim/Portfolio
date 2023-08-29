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
    public class RecentWorksController : BaseController<RecentWorksController>
    {

        public RecentWorksController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IActionResult> IndexAsync()
        {
            var recentWorks = await _unitOfWork.RecentWorkRepository.GetAsync(includeProperties: new List<string>() { "Image" });
            var recentWorkViewModels = _mapper.Map<List<RecentWorkViewModel>>(recentWorks);
            recentWorkViewModels.Reverse();
            return View(recentWorkViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateRecentWorkViewModel recentWorkViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            RecentWork recentWork = _mapper.Map<RecentWork>(recentWorkViewModel);
            string uniqueImageName = SaveImageAndGetTheImageName(recentWorkViewModel.ImageFile);
            Image recentWorkImage = new Image()
            {
                Name = uniqueImageName,
                Title = recentWork.Title,
                Alt = recentWork.Title + " image"
            };
            recentWork.Image = recentWorkImage;
            await _unitOfWork.RecentWorkRepository.AddAsync(recentWork);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync([FromForm] EditRecentWorkViewModel recentWorkViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            RecentWork recentWork = _mapper.Map<RecentWork>(recentWorkViewModel);

            if (recentWorkViewModel.ImageFile != null)
            {
                var image = await _unitOfWork.ImageRepository.GetByIdAsync(recentWorkViewModel.ImageId);
                DeleteImage(image.Name);
                _unitOfWork.ImageRepository.Delete(image);
                string uniqueImageName = SaveImageAndGetTheImageName(recentWorkViewModel.ImageFile);
                Image serviceImage = new Image()
                {
                    Name = uniqueImageName,
                    Title = recentWork.Title,
                    Alt = recentWork.Title + " image"
                };
                recentWork.Image = serviceImage;
            }
            _unitOfWork.RecentWorkRepository.Update(recentWork);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var recentWork = await _unitOfWork.RecentWorkRepository.GetFirstByFilter(s => s.Id == id, includeProperties: new List<string>() { "Image" });
            DeleteImage(recentWork.Image.Name);
            _unitOfWork.RecentWorkRepository.Delete(recentWork);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }
    }
}