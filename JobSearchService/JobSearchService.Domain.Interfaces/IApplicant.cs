using JobSearchService.Models.ViewModel;

namespace JobSearchService.Models.Interfaces
{
    public interface IApplicant
    {
        public Task<ApplicantJobView> Index();
        public Task<ApplicantPersonalInfoView> Edit(int id);
        public Task Edit(int id, ApplicantPersonalInfoView view);
    }
}
