using JobSearchService.Data;
using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationProfile> _userManager;
        private Task<ApplicationProfile> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CompanyController(ApplicationDbContext context, UserManager<ApplicationProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> Info(int id)
        {
            var company = await _context.Company.Include(l => l.Location).FirstOrDefaultAsync(c => c.Id == id);

            return View(company);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await GetCurrentUserAsync();
            var view = new EmployerCompanyView();

            var company = await _context.Company.FirstOrDefaultAsync(c => c.Id == user.CompanyId);

            var locationOptions = await _context.Location.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            }).ToListAsync();

            view.Company = company;
            view.LocationOptions = locationOptions;

            if (company == null || view == null)
            {
                return NotFound();
            }

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Company company)
        {
            try
            {
                var companyInfo = new Company()
                {
                    Id = company.Id,
                    CompanyName = company.CompanyName,
                    LocationId = company.LocationId,
                };

                _context.Company.Update(company);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return RedirectToAction("Index", "Employer");
        }
    }
}
