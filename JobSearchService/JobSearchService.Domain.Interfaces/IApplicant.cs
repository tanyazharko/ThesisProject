using Microsoft.AspNetCore.Mvc;

namespace JobSearchService
{ 
    public interface IApplicant
    {
        public Task<ApplicantJobView> Index();
        public Task<ApplicantPersonalInfoView> Edit(int id);
        public Task Edit(int id, ApplicantPersonalInfoView view);
        public Task Create(int id);
        public Task<ApplicantJobView> Info(int id, int JobId);
    }
}
