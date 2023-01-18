using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobSearchService.Models.ViewModel;

namespace JobSearchService.Models.Interfaces
{
    public interface ICompany
    {
        public Task<Company> Info(int id);
        public Task<EmployerCompanyView> Edit(int id);
        public Task Edit(int id, Company company);
    }
}
