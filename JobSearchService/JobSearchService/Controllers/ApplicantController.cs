using JobSearchService.Data;
using JobSearchService.Models.Interfaces;
using JobSearchService.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

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

