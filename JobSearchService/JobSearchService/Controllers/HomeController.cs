using JobSearchService.Data;
using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobSearchService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationProfile> _userManager;
        private Task<ApplicationProfile> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationProfile> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return View(user);
            }

            if (user.IsEmployer == true)
            {
                return RedirectToAction("Index", "Employer");
            }

            if (user.IsEmployer == false)
            {
                return RedirectToAction("Index", "Applicant");
            }

            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}