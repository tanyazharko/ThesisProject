using JobSearchService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService
{
    public class EmployerService : IEmployer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationProfile> _userManager;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        private Task<ApplicationProfile> GetCurrentUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        public EmployerService(ApplicationDbContext context, UserManager<ApplicationProfile> userManager, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CompanyJobView> Index()
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
            }).Where(j => j.CompanyId == user.CompanyId).ToListAsync();

            view.Company = await _context.Company.Include(a => a.ApplicationProfile).Include(l => l.Location).FirstOrDefaultAsync(c => c.Id == user.CompanyId);

            return view;
        }
    }
}
