using JobSearchService.Data;
using JobSearchService.Models.Interfaces;
using JobSearchService.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Models.Services
{
    public class CompanyService : ICompany
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationProfile> _userManager;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        private Task<ApplicationProfile> GetCurrentUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        public CompanyService(ApplicationDbContext context, UserManager<ApplicationProfile> userManager, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<EmployerCompanyView> Edit(int id)
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

            return view;
        }

        public async Task Edit(int id, Company company)
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

        public async Task<Company> Info(int id)
        {
            var company = await _context.Company.Include(l => l.Location).FirstOrDefaultAsync(c => c.Id == id);

            return company;
        }
    }
}
