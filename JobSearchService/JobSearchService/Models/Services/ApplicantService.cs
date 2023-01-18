using JobSearchService.Data;
using JobSearchService.Models.Interfaces;
using JobSearchService.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace JobSearchService.Models.Services
{
    public class ApplicantService : IApplicant
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationProfile> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Task<ApplicationProfile> GetCurrentUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        public ApplicantService(ApplicationDbContext context, UserManager<ApplicationProfile> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApplicantPersonalInfoView> Edit(int id)
        {
            var user = await GetCurrentUserAsync();
            var view = new ApplicantPersonalInfoView();

            var applicant = await _context.Applicant.FirstOrDefaultAsync(a => a.Id == user.ApplicantId);

            var locationOptions = await _context.Location.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            }).ToListAsync();

            view.LocationOptions = locationOptions;
            view.Id = applicant.Id;
            view.Applicant = applicant;
            view.ApplicationProfile = user;

            return view;
        }
        public async Task Edit(int id, ApplicantPersonalInfoView view)
        {
            var user = await _context.ApplicationUsers.Include(a => a.Applicant).FirstOrDefaultAsync(a => a.Id == view.ApplicationProfile.Id);

            user.FirstName = view.ApplicationProfile.FirstName;
            user.LastName = view.ApplicationProfile.LastName;
            user.LocationId = view.ApplicationProfile.LocationId;
            user.Applicant.Email = view.Applicant.Email;
            user.Applicant.SocialLink = view.Applicant.SocialLink;
            user.Applicant.Education = view.Applicant.Education;
            user.Applicant.Phone = view.Applicant.Phone;
            user.Applicant.Languages = view.Applicant.Languages;
            user.Applicant.Experience = view.Applicant.Experience;
            user.Applicant.HardSkills = view.Applicant.HardSkills;
            user.Applicant.Certifications = view.Applicant.Certifications;

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images");

            if (view.ResumeFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + view.ResumeFile.FileName;
                user.Applicant.ResumeLink = fileName;

                using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                {
                    await view.ResumeFile.CopyToAsync(fileStream);
                }
            }

            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicantJobView> Index()
        {
            var view = new ApplicantJobView();
            var user = await GetCurrentUserAsync();

            var jobs = await _context.Job.Include(c => c.Company).ThenInclude(l => l.Location).ToListAsync();

            var applicant = await _context.Applicant.Include(a => a.ApplicationProfile).ThenInclude(l => l.Location).FirstOrDefaultAsync(a => a.Id == user.ApplicantId);
            view.Jobs = jobs;
            view.ApplicationProfile = user;
            view.Applicant = applicant;

            return view;
        }

    }
}
