using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobSearchService.Models.ViewModel;

namespace JobSearchService.Models.Interfaces
{
    public interface IJob
    {
        public Task<List<Job>> Index(string positionSearchString, string companySearchString, string categorySearchString, string locationSearchString);
        public Task<JobEmploymentTypeView> Create();
        public Task Create(JobEmploymentTypeView view);
        public Task<Job> Delete(int id);
        public Task Delete(int id, Job job);
        public Task<JobEmploymentTypeView> Edit(int? id);
        public Task Edit(int id, Job job);
        public Task<Job> Info(int id);
    }
}
