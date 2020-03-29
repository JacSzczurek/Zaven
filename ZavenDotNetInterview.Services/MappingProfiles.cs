using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZavenDotNetInterview.Data.Models;
using ZavenDotNetInterview.Core.Models;

namespace ZavenDotNetInterview.Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<JobViewModel, Job>().ReverseMap();
            CreateMap<LogViewModel, Log>().ReverseMap();
        }
    }
}
