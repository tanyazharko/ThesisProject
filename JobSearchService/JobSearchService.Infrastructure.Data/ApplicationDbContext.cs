using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationProfile>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationProfile> ApplicationUsers { get; set; } = null!;
        public DbSet<Applicant> Applicant { get; set; } = null!;
        public DbSet<ApplicantJob> ApplicantJob { get; set; } = null!;
        public DbSet<Company> Company { get; set; } = null!;
        public DbSet<EmploymentType> EmploymentType { get; set; } = null!;
        public DbSet<Job> Job { get; set; } = null!;
        public DbSet<Location> Location { get; set; } = null!;
        public DbSet<JobCategory> Category { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobCategory>().HasData(
                  new JobCategory()
                  {
                      Id = 1,
                      Name = "Art",
                  },
                  new JobCategory()
                  {
                      Id = 2,
                      Name = "Programming",
                  },
                  new JobCategory()
                  {
                      Id = 3,
                      Name = "Management",
                  }
            );

            modelBuilder.Entity<EmploymentType>().HasData(
             new EmploymentType()
             {
                 Id = 1,
                 Name = "Full-timed",
             },
            new EmploymentType()
            {
                Id = 2,
                Name = "Part-time",
            }
          );

            modelBuilder.Entity<Location>().HasData(
          new Location()
          {
              Id = 1,
              Name = "Brazil",
          },
          new Location()
          {
              Id = 2,
              Name = "Belarus",
          },
          new Location()
          {
              Id = 3,
              Name = "Poland",
          }
      );
        }
    }
}
