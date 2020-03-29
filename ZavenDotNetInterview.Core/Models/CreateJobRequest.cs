using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZavenDotNetInterview.Core.Models
{
    public class CreateJobRequest
    {
        public string Name { get; set; }

        public DateTime? DoAfter { get; set; }
    }
}
