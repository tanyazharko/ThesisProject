using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobSearchService
{
    public class JobEmploymentTypeView
    {
        public int Id { get; set; }
        public Job Job { get; set; }
        public int? EmploymentTypeId { get; set; }
        public int? JobCategoryId { get; set; }
        public List<SelectListItem> EmploymentTypeOptions { get; set; }
        public List<SelectListItem> CategoryOptions { get; set; }
    }
}
