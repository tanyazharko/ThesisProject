namespace JobSearchService.Models.ViewModel
{
    public class JobsWithCountView
    {
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Salary { get; set; }
        public int? EmploymentTypeId { get; set; }
        public string EmploymentTypeName { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public string CompanyName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CompanyLocation { get; set; }
    }
}
