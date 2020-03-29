using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZavenDotNetInterview.Data.Models;
using System.Linq;
using System.Collections.Generic;
using ZavenDotNetInterview.Data;
using ZavenDotNetInterview.Services.Validators;

namespace ZavenDotNetInterview.Tests
{
    [TestClass]
    public class JobValidatorTests
    {
        [TestMethod]
        public void Job_Is_Correct()
        {
            var jobs = new List<Job>();
            var mockDbSet = GetMockDbSet<Job>(jobs);

            var mockContext = new Mock<ZavenDotNetInterviewContext>();
            mockContext.Setup(c => c.Jobs).Returns(mockDbSet.Object);

            var jobValidator = new JobValidator(mockContext.Object);

            var isValid = jobValidator.IsValid(new Core.Models.CreateJobRequest
            {
                Name = "Name",
                DoAfter = DateTime.Now.AddDays(1)
            }, out var _);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Job_Test_Name_Cannot_Be_Empty()
        {
            var jobs = new List<Job>();
            var mockDbSet = GetMockDbSet<Job>(jobs);

            var mockContext = new Mock<ZavenDotNetInterviewContext>();
            mockContext.Setup(c => c.Jobs).Returns(mockDbSet.Object);

            var jobValidator = new JobValidator(mockContext.Object);

            var isValid = jobValidator.IsValid(new Core.Models.CreateJobRequest
            {
                Name = ""
            }, out var _);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Job_Test_DoAfter_Must_Be_Greater_Than_Datetime_Now()
        {
            var jobs = new List<Job>();
            var mockDbSet = GetMockDbSet<Job>(jobs);

            var mockContext = new Mock<ZavenDotNetInterviewContext>();
            mockContext.Setup(c => c.Jobs).Returns(mockDbSet.Object);

            var jobValidator = new JobValidator(mockContext.Object);

            var isValid = jobValidator.IsValid(new Core.Models.CreateJobRequest
            {
                Name = "name",
                DoAfter = DateTime.Now.AddDays(-1)
            }, out var _);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Job_Test_Name_Must_Be_Unique()
        {
            var jobs = new List<Job>
            {
                new Job
                {
                    Name = "name"
                }
            };

            var mockDbSet = GetMockDbSet<Job>(jobs);

            var mockContext = new Mock<ZavenDotNetInterviewContext>();
            mockContext.Setup(c => c.Jobs).Returns(mockDbSet.Object);

            var jobValidator = new JobValidator(mockContext.Object);

            var isValid = jobValidator.IsValid(new Core.Models.CreateJobRequest
            {
                Name = "name"
            }, out var _);

            Assert.IsFalse(isValid);
        }

        private static Mock<DbSet<T>> GetMockDbSet<T>(ICollection<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(entities.Add);
            return mockSet;
        }
    }
}
