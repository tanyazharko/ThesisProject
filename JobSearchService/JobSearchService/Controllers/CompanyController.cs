using JobSearchService;
using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JobSearchService.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompany _company;

        public CompanyController(ICompany company)
        {
            _company = company;
        }

        public async Task<ActionResult> Info(int id)
        {
            return View(await _company.Info(id));
        }

        public async Task<IActionResult> Edit(int id)
        {
            EmployerCompanyView view = await _company.Edit(id);

            if (view == null)
            {
                return NotFound();
            }

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Company company)
        {
            try
            {
                await _company.Edit(id, company);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return RedirectToAction("Index", "Employer");
        }
    }
}
