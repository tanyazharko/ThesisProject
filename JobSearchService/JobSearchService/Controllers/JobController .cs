using JobSearchService.Areas.Identity.Pages.Account;
using JobSearchService;
using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace JobSearchService.Controllers
{
    public class JobController : Controller
    {
        private readonly IJob _job;

        public JobController(IJob job)
        {
            _job = job;
        }

        public async Task<IActionResult> Index(string positionSearchString, string companySearchString, string categorySearchString, string locationSearchString)
        {
            var view = await _job.Index(positionSearchString, companySearchString, categorySearchString, locationSearchString);

            return View(view);
        }
        public async Task<IActionResult> Create()
        {
            var view = await _job.Create();

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobEmploymentTypeView view)
        {
            try
            {
                await _job.Create(view);

                return RedirectToAction("Index", "Employer");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _job.Delete(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Job job)
        {
            try
            {
                await _job.Delete(id, job);

                return RedirectToAction("Index", "Employer");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var view =  await _job.Edit(id);
           
            if (id == null || view == null)
            {
                return NotFound();
            }

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Job job)
        {
            try
            {
                await _job.Edit(id, job);

                return RedirectToAction("Index", "Employer");
            }
            catch (DbUpdateConcurrencyException)
            {
                return View();
            }
        }

        public async Task<IActionResult> Info(int id)
        {
            return View(await _job.Info(id));
        }
    }
}
