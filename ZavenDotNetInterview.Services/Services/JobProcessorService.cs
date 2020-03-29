using System;
using System.Linq;
using System.Threading.Tasks;
using ZavenDotNetInterview.Data;
using ZavenDotNetInterview.Data.Models;
using ZavenDotNetInterview.Services.Interfaces;

namespace ZavenDotNetInterview.Services.Services
{
    public class JobProcessorService : IJobProcessorService
    {
        private ZavenDotNetInterviewContext _dbContext;
        private static Random _rand = new Random();
        private static JobStatus[] JobStatusesToProcess = new[] { JobStatus.New, JobStatus.Failed };

        public JobProcessorService(ZavenDotNetInterviewContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ProcessJobs()
        {
            var jobsToProcess = _dbContext.Jobs.Where(x => JobStatusesToProcess.Contains(x.Status) && (x.DoAfter == null || x.DoAfter < DateTime.Today)).ToList();
            
            foreach (var job in jobsToProcess)
            {
                var _ = Task.Run(async () =>
                {
                    var result = await ProcessJob(job);

                    job.Status = result;
                    _dbContext.SaveChanges();
                });

            }
        }        

        private async Task<JobStatus> ProcessJob(Job job, int counter = 0)
        {
            if (counter == 5)
            {
                LogJob(job, JobStatus.Closed);
                return JobStatus.Closed;
            }                

            if (_rand.Next(10) < 5)
            {
                await Task.Delay(2000);
                LogJob(job, JobStatus.Failed);
                return await ProcessJob(job, ++counter);
            }
            else
            {
                await Task.Delay(1000);
                LogJob(job, JobStatus.Done);
                return JobStatus.Done;
            }
        }

        private void LogJob(Job job, JobStatus status)
        {
            job.Logs.Add(new Log
            {
                CreatedAt = DateTime.Now,
                Description = $"JobStatus : {status}"
            });
        }
    }
}