namespace JobSearchService.Models
{
    public class ApplicantJobView
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public int ApplicantJobId { get; set; }
        public List<ApplicantJob> ApplicantJobs { get; set; }
        public Job Job { get; set; }
        public int JobId { get; set; }
        public List<Job> Jobs { get; set; }
        public ApplicantJob ApplicantJob { get; set; }
        public ApplicationProfile ApplicationProfile { get; set; }
    }
}
