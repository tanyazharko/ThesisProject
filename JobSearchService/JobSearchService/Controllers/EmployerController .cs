using JobSearchService.Data;
using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Controllers
{
    public class EmployerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationProfile> _userManager;
        private Task<ApplicationProfile> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public EmployerController(ApplicationDbContext context, UserManager<ApplicationProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var view = new CompanyJobView();

            view.Jobs = await _context.Job.Select(j => new JobsWithCountView()
                {
                JobId = j.Id,
                Position = j.Position,
                Description = j.Description,
                Salary = j.Salary,
                EmploymentTypeId = j.EmploymentTypeId,
                EmploymentTypeName = j.EmploymentType.Name,
                CategoryId = j.JobCategoryId,
                CategoryName = j.JobCategory.Name,
                CompanyId = j.CompanyId,
                Company = j.Company,
                CompanyName = j.Company.CompanyName,
                CompanyLocation = j.Company.Location.Name
            }).Where(j => j.CompanyId == user.CompanyId)
                .ToListAsync();

            view.Company = await _context.Company.Include(a => a.ApplicationProfile).Include(l => l.Location).FirstOrDefaultAsync(c => c.Id == user.CompanyId);

            return View(view);
        }
    }
}
