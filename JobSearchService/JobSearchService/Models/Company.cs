using System.ComponentModel.DataAnnotations;

namespace JobSearchService.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public int? LocationId { get; set; }
        public Location Location { get; set; }
        public List<Job> Jobs { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
