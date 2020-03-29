using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ZavenDotNetInterview.Core.Models;
using ZavenDotNetInterview.Data;
using ZavenDotNetInterview.Data.Models;
using ZavenDotNetInterview.Services.Interfaces;

namespace ZavenDotNetInterview.Services.Services
{
    public class JobService : IJobService
    {
        private readonly ZavenDotNetInterviewContext _dbContext;
        private readonly IMapper _mapper;


        public JobService(ZavenDotNetInterviewContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<JobViewModel> GetAllJobs()
        {
            var jobs = _dbContext.Jobs.ToList();
            return _mapper.Map<List<JobViewModel>>(jobs);
        }

        public void CreateJob(CreateJobRequest jobRequest)
        {
            var newJob = new Job()
            {
                DoAfter = jobRequest.DoAfter,
                Name = jobRequest.Name,
                Status = JobStatus.New,
                Logs = new List<Log>()
                {
                    new Log
                    {
                        CreatedAt = DateTime.Now,
                        Description = "Job created."
                    }
                }
            };

            _dbContext.Jobs.Add(newJob);
            _dbContext.SaveChanges();
        }
        
        public JobViewModel GetJob(Func<Job, bool> predicate)
        {
            var job = _dbContext.Jobs.FirstOrDefault(predicate);
            if (job == null)
                return null;

            return _mapper.Map<JobViewModel>(job);
        }
    }
}