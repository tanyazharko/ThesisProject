using JobSearchService.Data;
using JobSearchService.Models;
using JobSearchService.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Controllers
{
    public class EmployerController : Controller
    {
        private readonly IEmployer _employer;

        public EmployerController(IEmployer employer)
        {
            _employer = employer;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _employer.Index());
        }
    }
}
