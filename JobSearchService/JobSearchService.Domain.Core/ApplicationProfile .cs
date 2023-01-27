using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace JobSearchService
{
    public class ApplicationProfile : IdentityUser
    {
        [Required(ErrorMessage = "First Name is a Required Field")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is a Required Field")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        public int Age { get; set; }
        public int? LocationId { get; set; }
        public Location Location { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public bool IsEmployer { get; set; }
    }
}
