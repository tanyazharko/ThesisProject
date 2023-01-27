using JobSearchService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JobSearchService.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicant _applicant;

        public ApplicantController(IApplicant applicant)
        {
            _applicant = applicant;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _applicant.Index());
        }

        public async Task<IActionResult> Edit(int id)
        {
            ApplicantPersonalInfoView view = await _applicant.Edit(id);

            if (view == null)
            {
                return NotFound();
            }

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id)
        {
            try
            {
                await _applicant.Create(id);

                return RedirectToAction("Index", "Applicant");
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Info(int id, int JobId)
        {
            return View(await _applicant.Info(id, JobId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ApplicantPersonalInfoView view)
        {
            try
            {
                await _applicant.Edit(id,view);

                return RedirectToAction("Index", "Applicant");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}

