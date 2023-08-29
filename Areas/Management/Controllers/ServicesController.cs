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
    public class ServicesController : BaseController<ServicesController>
    {

        public ServicesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IActionResult> IndexAsync()
        {

            var services = await _unitOfWork.ServiceRepository.GetAsync(includeProperties: new List<string>() { "Image" });
            var serviceViewModels = _mapper.Map<List<ServiceViewModel>>(services);
            serviceViewModels.Reverse();
            return View(serviceViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateServiceViewModel createServiceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Service service = _mapper.Map<Service>(createServiceViewModel);
            string uniqueImageName = SaveImageAndGetTheImageName(createServiceViewModel.ImageFile);
            Image serviceImage = new Image()
            {
                Name = uniqueImageName,
                Title = service.Title,
                Alt = service.Title + " image"
            };
            service.Image = serviceImage;
            await _unitOfWork.ServiceRepository.AddAsync(service);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync([FromForm] EditServiceViewModel editServiceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Service service = _mapper.Map<Service>(editServiceViewModel);

            if (editServiceViewModel.ImageFile != null)
            {
                var image = await _unitOfWork.ImageRepository.GetByIdAsync(editServiceViewModel.ImageId);
                DeleteImage(image.Name);
                _unitOfWork.ImageRepository.Delete(image);
                string uniqueImageName = SaveImageAndGetTheImageName(editServiceViewModel.ImageFile);
                Image serviceImage = new Image()
                {
                    Name = uniqueImageName,
                    Title = service.Title,
                    Alt = service.Title + " image"
                };
                service.Image = serviceImage;
            }
            _unitOfWork.ServiceRepository.Update(service);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetFirstByFilter(s => s.Id == id, includeProperties: new List<string>() { "Image" });
            DeleteImage(service.Image.Name);
            _unitOfWork.ServiceRepository.Delete(service);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }

    }
}