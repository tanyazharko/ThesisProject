using JobSearchService.Areas.Identity.Pages.Account;
using JobSearchService.Data;
using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobSearchService.Controllers
{
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationProfile> _userManager;
        private readonly ILogger<JobController> _logger;
        private Task<ApplicationProfile> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public JobController(ApplicationDbContext context, UserManager<ApplicationProfile> userManager, ILogger<JobController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger= logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var jobs = from j in _context.Job.Include(c => c.Company).ThenInclude(l => l.Location).Include(et => et.EmploymentType).Include(ca => ca.JobCategory)  select j;

            return View(await jobs.ToListAsync());
        }

        public async Task<ActionResult> Create()
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

            return View(view);
        }

        [HttpPost]
        public async Task<ActionResult> Create(JobEmploymentTypeView view)
        {
            try
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

                _logger.LogInformation("The job has been added to the database.");

                return RedirectToAction("Index", "Employer");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var job = await _context.Job.Include(c => c.Company).Include(et => et.EmploymentType).Include(ca => ca.JobCategory).FirstOrDefaultAsync(j => j.Id == id);

            var user = await GetCurrentUserAsync();

            if (job.CompanyId != user.CompanyId)
            {
                return NotFound();
            }

            return View(job);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, Job job)
        {
            try
            {
                _context.Job.Remove(job);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Job has been deleted.");

                return RedirectToAction("Index", "Employer");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int? id)
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

            if (id == null || view == null)
            {
                return NotFound();
            }

            view.EmploymentTypeOptions = employmentTypeOptions;
            view.CategoryOptions = categoryOptions;
            view.Id = job.Id;
            view.Job = job;

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Job job)
        {
            try
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
                return RedirectToAction("Index", "Employer");
            }
            catch (DbUpdateConcurrencyException)
            {
                return View();
            }
        }

        public async Task<ActionResult> Info(int id)
        {
            var job = await _context.Job.Include(c => c.Company).Include(et => et.EmploymentType).Include(ca => ca.JobCategory).Include(aj => aj.ApplicantJobs)
                .ThenInclude(aj => aj.Applicant).ThenInclude(au => au.ApplicationProfile).FirstOrDefaultAsync(j => j.Id == id);

            return View(job);
        }
        
    }
}
