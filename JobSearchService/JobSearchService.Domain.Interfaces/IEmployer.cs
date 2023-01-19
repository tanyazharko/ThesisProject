using JobSearchService.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Models.Interfaces
{
    public interface IEmployer
    {
        public Task<CompanyJobView> Index();
    }
}
