using JobSearchService.Data;
using JobSearchService.Models.Interfaces;
using JobSearchService.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Models.Services
{
    public class JobService : IJob
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationProfile> _userManager;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        private ApplicationDbContext context;

        private Task<ApplicationProfile> GetCurrentUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        public JobService(ApplicationDbContext context, UserManager<ApplicationProfile> userManager, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<JobEmploymentTypeView> Create()
        {
            var view = new JobEmploymentTypeView();

            var employmentTypeOptions = await _context.EmploymentType.Select(et => new SelectListItem()
            {
                Text = et.Name,
                Value = et.Id.ToString()
            }).ToListAsync();

            var categoryOptions = await _context.Category.Select(ca => new SelectListItem()
            {
                Text = ca.Name,
                Value = ca.Id.ToString()
            }).ToListAsync();

            view.EmploymentTypeOptions = employmentTypeOptions;
            view.CategoryOptions = categoryOptions;

            return view;
        }

        public async Task Create(JobEmploymentTypeView view)
        {
            var user = await GetCurrentUserAsync();

            var jobPost = new Job
            {
                Position = view.Job.Position,
                Description = view.Job.Description,
                Salary = view.Job.Salary,
                EmploymentTypeId = view.EmploymentTypeId,
                JobCategoryId = view.JobCategoryId,
                CompanyId = user.CompanyId
            };

            _context.Add(jobPost);
            await _context.SaveChangesAsync();
        }

        public async Task<Job> Delete(int id)
        {
            var job = await _context.Job.Include(c => c.Company).Include(et => et.EmploymentType).Include(ca => ca.JobCategory).FirstOrDefaultAsync(j => j.Id == id);

            var user = await GetCurrentUserAsync();

            return job;
        }

        public async Task Delete(int id, Job job)
        {
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
        }

        public async Task<JobEmploymentTypeView> Edit(int? id)
        {
            var view = new JobEmploymentTypeView();
            var job = await _context.Job.FirstOrDefaultAsync(j => j.Id == id);

            var employmentTypeOptions = await _context.EmploymentType.Select(et => new SelectListItem()
            {
                Text = et.Name,
                Value = et.Id.ToString()
            }).ToListAsync();

            var categoryOptions = await _context.Category.Select(ca => new SelectListItem()
            {
                Text = ca.Name,
                Value = ca.Id.ToString()
            }).ToListAsync();

            view.EmploymentTypeOptions = employmentTypeOptions;
            view.CategoryOptions = categoryOptions;
            view.Id = job.Id;
            view.Job = job;

            return view;
        }

        public async Task Edit(int id, Job job)
        {
            var user = await GetCurrentUserAsync();

            var singleJob = await _context.Job.Include(c => c.Company).Include(et => et.EmploymentType).Include(ca => ca.JobCategory).Include(aj => aj.ApplicantJobs)
            .ThenInclude(aj => aj.Applicant).ThenInclude(au => au.ApplicationProfile).FirstOrDefaultAsync(j => j.Id == id);

            singleJob.Position = job.Position;
            singleJob.Description = job.Description;
            singleJob.Salary = job.Salary;
            singleJob.EmploymentTypeId = job.EmploymentTypeId;
            singleJob.JobCategoryId = job.JobCategoryId;
            singleJob.CompanyId = user.CompanyId;

            _context.Update(singleJob);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Job>> Index(string positionSearchString, string companySearchString, string categorySearchString, string locationSearchString)
        {
            var user = await GetCurrentUserAsync();
            var jobs = from j in _context.Job.Include(c => c.Company).ThenInclude(l => l.Location).Include(et => et.EmploymentType).Include(ca => ca.JobCategory) select j;

            if (!string.IsNullOrEmpty(positionSearchString))
            {
                jobs = jobs.Where(s => s.Position.Contains(positionSearchString));
            }

            if (!string.IsNullOrEmpty(companySearchString))
            {
                jobs = jobs.Where(s => s.Company.CompanyName.Contains(companySearchString));
            }

            if (!string.IsNullOrEmpty(categorySearchString))
            {
                jobs = jobs.Where(s => s.JobCategory.Name.Contains(categorySearchString));
            }

            if (!string.IsNullOrEmpty(locationSearchString))
            {
                jobs = jobs.Where(s => s.Company.Location.Name.Contains(locationSearchString));
            }

            return await jobs.ToListAsync();
        }

        public async Task<Job> Info(int id)
        {
            var job = await _context.Job.Include(c => c.Company).Include(et => et.EmploymentType).Include(ca => ca.JobCategory).Include(aj => aj.ApplicantJobs)
                           .ThenInclude(aj => aj.Applicant).ThenInclude(au => au.ApplicationProfile).FirstOrDefaultAsync(j => j.Id == id);

            return job;
        }
    }
}
