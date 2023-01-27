using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService
{
    public interface IEmployer
    {
        public Task<CompanyJobView> Index();
    }
}
