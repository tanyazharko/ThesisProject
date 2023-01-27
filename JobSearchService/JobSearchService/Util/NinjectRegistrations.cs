using JobSearchService;
using Ninject.Modules;

namespace JobSearchService.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplicant>().To<ApplicantService>();
            Bind<ICompany>().To<CompanyService>();
            Bind<IEmployer>().To<EmployerService>();
            Bind<IJob>().To<JobService>();
        }
    }
}
