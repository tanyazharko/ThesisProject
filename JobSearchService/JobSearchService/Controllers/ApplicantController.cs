using JobSearchService.Data;
using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Controllers
{
    public class ApplicantController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ApplicantController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var applicantJob = new ApplicantJob();

            var applicant = await _context.Applicant.Include(a => a.ApplicationUser).ThenInclude(l => l.Location).FirstOrDefaultAsync(a => a.Id == user.ApplicantId);

            var applicantJobs = await _context.ApplicantJob.Include(j => j.Job).Where(a => a.Applicant.ApplicationUser.Id == user.Id).ToListAsync();

            var jobs = await _context.Job.Include(c => c.Company).ThenInclude(l => l.Location).Include(aj => aj.ApplicantJobs).ToListAsync();

            applicantJob.ApplicantJobs = applicantJobs;
            applicantJob.Jobs = jobs;
            applicantJob.ApplicationUser = user;
            applicantJob.Applicant = applicant;

            return View(applicantJob);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(int id)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var jobApplication = new ApplicantJob
                {
                    ApplicantId = user.ApplicantId,
                    JobId = id
                };
                _context.Add(jobApplication);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Applicant");
            }
            catch
            {
                return View();
            }
        }

    }
}
