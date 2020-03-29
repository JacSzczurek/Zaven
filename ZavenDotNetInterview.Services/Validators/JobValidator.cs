using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZavenDotNetInterview.Core.Models;
using ZavenDotNetInterview.Data;
using ZavenDotNetInterview.Services.Interfaces;

namespace ZavenDotNetInterview.Services.Validators
{
    public class JobValidator : IJobValidator
    {
        private readonly ZavenDotNetInterviewContext _dbContext;

        public JobValidator(ZavenDotNetInterviewContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsValid(CreateJobRequest jobRequest, out Dictionary<string, string> validationMessages)
        {
            validationMessages = new Dictionary<string, string>();

            var nameIsEmpty = String.IsNullOrEmpty(jobRequest.Name);

            if (nameIsEmpty)
                validationMessages.Add("Name", ValidationMessages.JobNameIsEmpty);

            var nameExists = _dbContext.Jobs.Any(ctx => ctx.Name == jobRequest.Name);

            if (nameExists && !nameIsEmpty)
                validationMessages.Add("Name", ValidationMessages.JobNameAlreadyExists);
            
            var doAfterLower = jobRequest.DoAfter != null && jobRequest.DoAfter <= DateTime.Now;

            if (doAfterLower)
                validationMessages.Add("DoAfter", ValidationMessages.DoAfterMustBeGreaterThanDateNow);

            return !validationMessages.Any();
        }
    }
}
