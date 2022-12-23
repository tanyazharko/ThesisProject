using System.ComponentModel.DataAnnotations;

namespace JobSearchService.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect address")]
        public string Email { get; set; }
        public string Education { get; set; }
        public string SkillsAndCertifications { get; set; }
        public string ResumeLink { get; set; }
        public string SocialLink { get; set; }

        [StringLength(500, ErrorMessage = "The Experience must be less than 500 characters.")]
        public string Experience { get; set; }
        public List<ApplicantJob> ApplicantJobs { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int JobId { get; set; }
    }
}
