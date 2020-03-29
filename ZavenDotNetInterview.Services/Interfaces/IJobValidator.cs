using System.Collections.Generic;
using ZavenDotNetInterview.Core.Models;

namespace ZavenDotNetInterview.Services.Interfaces
{
    public interface IJobValidator
    {
        bool IsValid(CreateJobRequest jobRequest, out Dictionary<string, string> validationMessages);
    }
}