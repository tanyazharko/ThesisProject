namespace JobSearchService.Models
{
    public class CompanyJobView
    {
        public List<JobsWithCountView> Jobs { get; set; }
        public Company Company { get; set; }
    }
}
