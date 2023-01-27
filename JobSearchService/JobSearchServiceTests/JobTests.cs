using Xunit;
using JobSearchService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using JobSearchService;

namespace JobSearchServiceTests
{
    public class JobTests
    {
        [Fact]
        public void TestGetJobId()
        {
            Job testJob1 = new Job();
            testJob1.Id = 1;
            Assert.Equal(1, testJob1.Id);
        }

        [Fact]
        public void TestSetJobId()
        {
            Job testJob2 = new Job();
            testJob2.Id = 1;
            testJob2.Id = 2;
            Assert.Equal(2, testJob2.Id);
        }

        [Fact]
        public void TestGetJobSalary()
        {
            Job testJob3 = new Job();
            testJob3.Salary = "1000";
            Assert.Equal("1000", testJob3.Salary);
        }

        [Fact]
        public void TestSetJobSalary()
        {
            Job testJob4 = new Job();
            testJob4.Salary = "1000";
            testJob4.Salary = "2000";
            Assert.Equal("2000", testJob4.Salary);
        }

        public void TestGetJobPosition()
        {
            Job testJob5 = new Job();
            testJob5.Position = "Test";
            Assert.Equal("Test", testJob5.Position);
        }

        [Fact]
        public void TestSetJobPosition()
        {
            Job testJob6 = new Job();
            testJob6.Position = "Test";
            testJob6.Position = "Test1";
            Assert.Equal("Test1", testJob6.Position);
        }

        public void TestGetEmploymentTypeId()
        {
            Job testJob7 = new Job();
            testJob7.EmploymentTypeId = 1;
            Assert.Equal(1, testJob7.EmploymentTypeId);
        }

        [Fact]
        public void TestSetEmploymentTypeId()
        {
            Job testJob8 = new Job();
            testJob8.EmploymentTypeId = 1;
            testJob8.EmploymentTypeId = 2;
            Assert.Equal(2, testJob8.EmploymentTypeId);
        }
        public void TestGetCompanyId()
        {
            Job testJob9 = new Job();
            testJob9.CompanyId = 1;
            Assert.Equal(1, testJob9.CompanyId);
        }

        [Fact]
        public void TestSetCompanyId()
        {
            Job testJob10 = new Job();
            testJob10.CompanyId = 1;
            testJob10.CompanyId = 2;
            Assert.Equal(2, testJob10.CompanyId);
        }

        public void TestGetJobCategoryId()
        {
            Job testJob11 = new Job();
            testJob11.JobCategoryId = 1;
            Assert.Equal(1,testJob11.JobCategoryId);
        }

        [Fact]
        public void TestSetJobCategoryId()
        {
            Job testJob12 = new Job();
            testJob12.JobCategoryId = 1;
            testJob12.JobCategoryId = 2;
            Assert.Equal(2, testJob12.JobCategoryId);
        }

        public void TestGetDescription()
        {
            Job testJob13 = new Job();
            testJob13.Description = "aaa";
            Assert.Equal("aaa", testJob13.Description);
        }

        [Fact]
        public void TestSetDescription()
        {
            Job testJob14 = new Job();
            testJob14.Description = "aaa";
            testJob14.Description = "bbb";
            Assert.Equal("bbb", testJob14.Description);
        }

        //[Fact]
        //public async void TestCreateJob()
        //{
        //    DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateJob").Options;

        //    using (ApplicationDbContext context = new ApplicationDbContext(options))
        //    {
        //        JobEmploymentTypeView testJob1 = new JobEmploymentTypeView();
        //        testJob1.Job.Id = 1;
        //        testJob1.Job.Salary = "121000";
        //        testJob1.Job.Position = "Тестировщик";
        //        testJob1.Job.Description = "знания английского ";
        //        testJob1.Job.CompanyId = 1;
        //        testJob1.Job.EmploymentTypeId = 1;
        //        testJob1.Job.JobCategoryId = 2;
                
        //        JobService jobService = new JobService(context);

        //        await jobService.Create(testJob1);

        //        var jobAnswer = context.Job.FirstOrDefault(a => a.Id == testJob1.Id);

        //        Assert.Equal(testJob1, jobAnswer);
        //    }
        //}
    }
}