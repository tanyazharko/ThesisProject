using JobSearchService.Data;
using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ApplicantJob applicantJob = new ApplicantJob();

            var applicant = await _context.Applicant.Include(a => a.ApplicationUser).ThenInclude(l => l.Location).FirstOrDefaultAsync(a => a.Id == user.ApplicantId);

            var applicantJobs = await _context.ApplicantJob.Include(j => j.Job).Where(a => a.Applicant.ApplicationUser.Id == user.Id).ToListAsync();

            var jobs = await _context.Job.Include(c => c.Company).ThenInclude(l => l.Location).Include(a => a.ApplicantJobs).ToListAsync();

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
                ApplicationUser user = await GetCurrentUserAsync();

                ApplicantJob jobApplication = new ApplicantJob
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

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, ApplicantJob applicantJob)
        {
            try
            {
                _context.ApplicantJob.Remove(applicantJob);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await GetCurrentUserAsync();
            var applicationUser = new ApplicationUser();
            var applicant = await _context.Applicant.FirstOrDefaultAsync(a => a.Id == user.ApplicantId);

            if (applicant == null)
            {
                return NotFound();
            }

            applicationUser.Applicant = applicant;
            applicationUser = user;

            return View(applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationUser applicationUser)
        {
            try
            {
                var user = await _context.ApplicationUsers
                    .Include(a => a.Applicant)
                    .FirstOrDefaultAsync(a => a.Id == applicationUser.Id);

                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.LocationId = applicationUser.LocationId;
                user.Applicant.Email = applicationUser.Applicant.Email;
                user.Applicant.SocialLink = applicationUser.Applicant.SocialLink;
                user.Applicant.Education = applicationUser.Applicant.Education;
                user.Applicant.Experience = applicationUser.Applicant.Experience;
                user.Applicant.SkillsAndCertifications = applicationUser.Applicant.SkillsAndCertifications;


                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images");
               
                if (applicationUser.ResumeFile != null)
                {
                    var fileName = Guid.NewGuid().ToString() + applicationUser.ResumeFile.FileName;
                    user.Applicant.ResumeLink = fileName;
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                    {
                        await applicationUser.ResumeFile.CopyToAsync(fileStream);
                    }
                }
               
                _context.ApplicationUsers.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Applicant");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}

