using JobSearchService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobSearchService.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<ApplicantJob> ApplicantJob { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<EmploymentType> EmploymentType { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationUser user = new ApplicationUser
            {
                Id = "1",
                FirstName = "Tanya",
                LastName = "Zharko",
                Age = 21,
                PhoneNumber = "2222222",
                Email = "tanyazharko@gmail.com"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "12345");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<Category>().HasData(
                  new Category()
                  {
                      Id = 1,
                      Name = "Art",
                  },
                  new Category()
                  {
                      Id = 2,
                      Name = "Programming",
                  },
                  new Category()
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
