using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobSearchService.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        public string? CompanyName { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public List<Job> Jobs { get; set; }
        public ApplicationProfile? ApplicationProfile { get; set; }
        
        [DisplayName("Company Site")]
        public string? CompanySize { get; set; }
        public bool HasMentor { get; set; }
        public bool HasProfDev { get; set; }
    }
}
