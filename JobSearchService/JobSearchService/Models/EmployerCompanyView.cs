using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobSearchService.Models
{
    public class EmployerCompanyView
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int? LocationId { get; set; }
        public List<SelectListItem> LocationOptions { get; set; }
    }
}
