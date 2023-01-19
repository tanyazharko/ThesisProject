using System.ComponentModel.DataAnnotations;

namespace JobSearchService.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Position is a Required Field")]
        public string? Position { get; set; }

        [StringLength(200, ErrorMessage = "The Description must be less than 200 characters.")]
        public string? Description { get; set; }
        public string? Salary { get; set; }
        public int? EmploymentTypeId { get; set; }
        [Required(ErrorMessage = "Employment Type is a Required Field")]
        public EmploymentType EmploymentType { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? JobCategoryId { get; set; }
        [Required(ErrorMessage = "Category is a Required Field")]
        public JobCategory JobCategory { get; set; }
        public List<ApplicantJob> ApplicantJobs { get; set; }
    }
}
