using System;
using System.Collections.Generic;
using ZavenDotNetInterview.Core.Models;
using ZavenDotNetInterview.Data.Models;

namespace ZavenDotNetInterview.Services.Interfaces
{
    public interface IJobService
    {
        void CreateJob(CreateJobRequest jobRequest);
        List<JobViewModel> GetAllJobs();
        JobViewModel GetJob(Func<Job, bool> predicate);
    }
}