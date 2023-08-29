using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.UnitOfWork;

namespace Portfolio
{
    public abstract class BaseController<ControllerType> : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;

        protected BaseController(IUnitOfWork unitOfWork = null, IMapper mapper = null, IConfiguration configuration = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        protected static readonly string _imagesPathForSave = "./wwwroot/Images";

        protected static string SaveImageAndGetTheImageName(IFormFile image)
        {
            string uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(_imagesPathForSave, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public static void DeleteImage(string imageName)
        {
            string path = Path.Combine("wwwroot", "Images", imageName);
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting image file {imageName}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Image file {imageName} does not exist.");
            }
        }
    }

}