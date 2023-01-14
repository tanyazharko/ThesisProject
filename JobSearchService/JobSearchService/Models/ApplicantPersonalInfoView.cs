using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobSearchService.Models
{
    public class ApplicantPersonalInfoView
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public ApplicationProfile ApplicationProfile { get; set; }
        public int? LocationId { get; set; }
        public List<SelectListItem> LocationOptions { get; set; }
        public IFormFile ResumeFile { get; set; }
    }
}
