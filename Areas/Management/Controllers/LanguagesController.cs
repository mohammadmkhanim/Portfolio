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
    public class LanguagesController : BaseController<LanguagesController>
    {

        public LanguagesController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(unitOfWork, mapper, configuration)
        {
        }

        public async Task<IActionResult> IndexAsync()
        {
            var languages = await _unitOfWork.LanguageRepository.GetAsync(includeProperties: new List<string>() { "Image" });
            var languageViewModels = _mapper.Map<List<LanguageViewModel>>(languages);
            languageViewModels.Reverse();
            return View(languageViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateLanguageViewModel languageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Language language = _mapper.Map<Language>(languageViewModel);
            string uniqueImageName = SaveImageAndGetTheImageName(languageViewModel.ImageFile);
            Image serviceImage = new Image()
            {
                Name = uniqueImageName,
                Title = language.Title,
                Alt = language.Title + " image"
            };
            language.Image = serviceImage;
            await _unitOfWork.LanguageRepository.AddAsync(language);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync([FromForm] EditLanguageViewModel languageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Language language = _mapper.Map<Language>(languageViewModel);

            if (languageViewModel.ImageFile != null)
            {
                var image = await _unitOfWork.ImageRepository.GetByIdAsync(languageViewModel.ImageId);
                DeleteImage(image.Name);
                await _unitOfWork.ImageRepository.DeleteAsync(languageViewModel.ImageId);

                string uniqueImageName = SaveImageAndGetTheImageName(languageViewModel.ImageFile);
                Image languageImage = new Image()
                {
                    Name = uniqueImageName,
                    Title = language.Title,
                    Alt = language.Title + " image"
                };
                language.Image = languageImage;
                await _unitOfWork.ImageRepository.AddAsync(languageImage);
            }
            _unitOfWork.LanguageRepository.Update(language);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var language = await _unitOfWork.LanguageRepository.GetFirstByFilter(s => s.Id == id, includeProperties: new List<string>() { "Image" });
            DeleteImage(language.Image.Name);
            _unitOfWork.LanguageRepository.Delete(language);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }
    }
}