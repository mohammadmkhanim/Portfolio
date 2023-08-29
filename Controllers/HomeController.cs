using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio;
using Portfolio.Data.UnitOfWork;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class HomeController : BaseController<HomeController>
{

    public HomeController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {

    }

    public async Task<IActionResult> Index()
    {
        var services = await _unitOfWork.ServiceRepository.GetAsync(includeProperties: new List<string>() { "Image" });
        services.Reverse();
        ViewBag.Services = _mapper.Map<List<ServiceViewModel>>(services);

        var recentWorks = await _unitOfWork.RecentWorkRepository.GetAsync(includeProperties: new List<string>() { "Image" });
        recentWorks.Reverse();
        ViewBag.RecentWorks = _mapper.Map<List<RecentWorkViewModel>>(recentWorks);

        var technicalSkills = await _unitOfWork.TechnicalSkillRepository.GetAsync();
        ViewBag.TechnicalSkills = _mapper.Map<List<TechnicalSkillViewModel>>(technicalSkills);

        var languages = await _unitOfWork.LanguageRepository.GetAsync(includeProperties: new List<string>() { "Image" });
        ViewBag.Languages = _mapper.Map<List<LanguageViewModel>>(languages);

        return View();
    }

    public IActionResult DownloadCV()
    {
        string filePath = "./wwwroot/DownloadFiles/resume.pdf"; 
        string mimeType = "application/pdf";
        return File(System.IO.File.OpenRead(filePath), mimeType, Path.GetFileName(filePath));
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
